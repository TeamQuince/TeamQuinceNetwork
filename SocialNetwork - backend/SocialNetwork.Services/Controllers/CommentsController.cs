namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Models.BindingModels;
    using Models.BindingModels.Comment;
    using Models.ViewModels;
    using Models.ViewModels.CommentViewModels;
    using SocialNetwork.Models;
    using Filters;

    /// <summary>
    /// Created on September 2, 2015 by Idmitrov
    /// </summary>
    [Route("api/posts/{id}/comments")]
    [Authorize]
    public class CommentsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetComments([FromUri]int id)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null) 
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var commentsPreviews = post.Comments
                .OrderBy(c => c.PostedOn)
                .AsQueryable()
                .Select(p => CommentViewModel.CreatePreview(currentUser, p))
                .ToList();

            if (!commentsPreviews.Any())
            {
                return this.StatusCode(HttpStatusCode.NoContent);
            }

            return this.Ok(commentsPreviews);
        }

        [ValidateModel]
        [HttpPost]
        public IHttpActionResult AddComment([FromUri] int id, [FromBody] AddCommentBindingModel model)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var newCommentToAdd = new Comment()
            {
                Author = currentUser,
                Post = post,
                PostedOn = DateTime.Now,
                Content = model.CommentContent
            };

            post.Comments.Add(newCommentToAdd);
            this.Data.SaveChanges();

            var newCommentViewModel = this.Data.Comments
                .Where(c => c.Id == newCommentToAdd.Id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();

            return this.Ok(newCommentViewModel);
        }

        [Route("api/posts/{postId}/comments/{commentId}")]
        [ValidateModel]
        [HttpPut]
        public IHttpActionResult EditComment([FromUri] int postId, int commentId, [FromBody] AddCommentBindingModel model)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (postComment == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (postComment.Author.Id != currentUserId)
            {
                return this.BadRequest("Not allowed. User must be author of comment.");
            }

            postComment.Content = model.CommentContent;
            this.Data.SaveChanges();

            var postCommentViewModel = this.Data.Comments.Where(c => c.Id == postComment.Id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();
            postCommentViewModel.PostId = post.Id;

            return this.Ok(postCommentViewModel);
        }

        [Route("api/posts/{postId}/comments/{commentId}")]
        [HttpDelete]
        public IHttpActionResult DeleteComment([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (postComment == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (postComment.Author.Id != currentUserId)
            {
                return this.BadRequest("Not allowed. User must be author of comment or wall-owner.");
            }

            var likes = postComment.Likes.ToList();
            foreach (var like in likes)
            {
                this.Data.CommentLikes.Remove(like);
            }

            postComment.Likes.Clear();
            post.Comments.Remove(postComment);
            this.Data.Comments.Remove(postComment);
            this.Data.SaveChanges();

            return this.Ok("Comment deleted.");
        }

        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpPost]
        public IHttpActionResult LikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (postComment == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var postCommentLikes = this.Data.CommentLikes
                .Where(cl => cl.Comment.Id == postComment.Id);

            if (!postComment.Author.Friends.Contains(currentUser) && !post.Owner.Friends.Contains(currentUser))
            {
                return this.BadRequest("Not allowed. Either wall-owner or author must be friends.");
            }

            if (postCommentLikes.Any(l => l.Author.Id == currentUserId))
            {
                return this.BadRequest("Comment is already liked.");
            }

            var newLikeToAdd = new CommentLike()
            {
                Author = currentUser
            };

            postComment.Likes.Add(newLikeToAdd);
            this.Data.SaveChanges();

            var commentLikeViewModel = this.Data.Comments.Where(c => c.Id == commentId)
                .Select(LikedCommentViewModel.Create);

            return this.Ok(commentLikeViewModel);
        }

        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpDelete]
        public IHttpActionResult UnlikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.Posts.Find(postId);
            if (post == null)
            {
                return this.NotFound();
            }

            var comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var likes = this.Data.CommentLikes.Where(cl => cl.Comment.Id == commentId);

            var likeToRemove = likes.FirstOrDefault(l => l.Author.Id == currentUserId);
            if (likeToRemove == null)
            {
                return this.BadRequest("Comment is not liked.");
            }

            comment.Likes.Remove(likeToRemove);
            this.Data.CommentLikes.Remove(likeToRemove);
            this.Data.SaveChanges();

            var likesViewModel = this.Data.Comments.Where(c => c.Id == commentId)
                .Select(UnlikedCommendViewModel.Create);

            return this.Ok(likesViewModel);
        }

        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpGet]
        public IHttpActionResult CommentLikesDetailed([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return this.NotFound();
            }

            var postCommentlikes = this.Data.CommentLikes.Where(cl => cl.Comment.Id == commentId)
                .Select(Models.ViewModels.CommentViewModels.CommentLikeViewModel.Create)
                .ToList(); ;

            return this.Ok(postCommentlikes);
        }

        [Route("api/posts/{postId}/comments/{commentId}/likes/preview")]
        [HttpGet]
        public IHttpActionResult CommentLikesPreview([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var comment = post.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return this.NotFound();
            }

            var postCommentLikesPreview = this.Data.Comments
                .Where(c => c.Id == commentId)
                .Take(10)
                .Select(CommentLikePreviewViewModel.Create)
                .ToList();

            return this.Ok(postCommentLikesPreview);
        }
    }
}
