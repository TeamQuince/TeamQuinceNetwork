/*
 *  Created on September 2, 2015 by Idmitrov
 */
namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Filters;
    using Models.BindingModels.Comment;
    using Models.ViewModels.Comment;

    [Route("api/posts/{id}/comments")]
    [Authorize]
    public class CommentsController : BaseApiController
    {
        // GET COMMENTS => GET => api/posts/id/comments
        [HttpGet]
        public IHttpActionResult GetComments([FromUri]int id)
        {
            // POST
            var post = this.Data.Posts.Find(id);

            // POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", id));

            // COMMENTS
            var postComments = this.Data.Comments.Where(c => c.Post.Id == post.Id)
                .Select(CommentViewModel.Create);

            // NO COMMENTS
            if (!postComments.Any()) return this.StatusCode(HttpStatusCode.NoContent);

            return this.Ok(postComments);
        }

        // ADD COMMENT => POST => api/posts/id/comments
        [ValidateModel]
        [HttpPost]
        public IHttpActionResult AddComment([FromUri] int id, [FromBody] AddCommentBindingModel model)
        {
            // POST
            var post = this.Data.Posts.Find(id);

            // POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", id));

            // LOGGED USER
            var loggedUser = this.User.Identity.GetUserId();

            // NEW COMMENT TO ADD
            var newCommentToAdd = new Comment()
            {
                Author = this.Data.Users.Find(loggedUser),
                Post = post,
                PostedOn = DateTime.Now,
                Content = model.CommentContent
            };

            // ADD COMMENT AND SAVE
            post.Comments.Add(newCommentToAdd);
            this.Data.SaveChanges();

            // NEW COMMENT VIEW MODEL
            var newCommentViewModel = this.Data.Comments.Where(c => c.Id == newCommentToAdd.Id)
                .Select(CommentViewModel.Create);

            return this.Created("AddComment", newCommentViewModel);
        }

        // EDIT COMMENT => PUT => api/posts/id/comments/id
        [Route("api/posts/{postId}/comments/{commentId}")]
        [ValidateModel]
        [HttpPut]
        public IHttpActionResult EditComment(
            [FromUri] int postId, int commentId, [FromBody] AddCommentBindingModel model)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // IF POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // IF COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", commentId));

            // LOGGED USER
            var loggedUser = this.User.Identity.GetUserId();

            // IF LOGGED USER IS NOT COMMENT AUTHOR
            if (postComment.Author.Id != loggedUser) return this.Unauthorized();

            // CHANGE CONTENT AND SAVE
            postComment.Content = model.CommentContent;
            this.Data.SaveChanges();

            // COMMENT VIEW MODEL
            var postCommentViewModel = this.Data.Comments.Where(c => c.Id == postComment.Id)
                .Select(CommentViewModel.Create);

            return this.Ok(postCommentViewModel);
        }

        // DELETE COMMENT => DELETE => api/posts/id/comments/id
        [Route("api/posts/{postId}/comments/{commentId}")]
        [HttpDelete]
        public IHttpActionResult DeleteComment([FromUri] int postId, [FromUri] int commentId)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // IF POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // IF COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", commentId));

            // LOGGED USER
            var loggedUser = this.User.Identity.GetUserId();

            // IF LOGGED USER IS COMMENT AUTHOR
            if (postComment.Author.Id != loggedUser) return this.Unauthorized();

            // DELETE COMMENT AND SAVE
            this.Data.Comments.Remove(postComment);
            this.Data.SaveChanges();

            return this.Ok("Comment successfully deleted.");
        }

        // LIKE COMMENT => POST => api/posts/id/comments/id/likes
        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpPost]
        public IHttpActionResult LikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // IF POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // IF COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", commentId));

            // LOGGED USER
            var loggedUser = this.User.Identity.GetUserId();

            // COMMET LIKES
            var postCommentLikes = this.Data.CommentLikes.Where(cl => cl.Comment.Id == postComment.Id);

            // IF COMMENT LIKES CONTAINS AUTHOR AS LOGGED USER => COMMENT IS ALREADY LIKED
            if (postCommentLikes.Any(l => l.Author.Id == loggedUser)) return this.BadRequest("Comment is already liked");

            // NEW LIKE TO ADD
            var newLikeToAdd = new CommentLike()
            {
                Author = this.Data.Users.FirstOrDefault(u => u.Id == loggedUser),
                Comment = postComment
            };

            // COMMENT LIKES ADD NEW LIKE AND SAVE
            this.Data.CommentLikes.Add(newLikeToAdd);
            this.Data.SaveChanges();

            // LIKES VIEW MODEL
            var commentLikeViewModel = this.Data.Comments.Where(c => c.Id == commentId)
                .Select(LikedCommentViewModel.Create);

            return this.Ok(commentLikeViewModel);
        }

        // UNLIKE COMMENT => DELETE => api/posts/id/comments/id/likes
        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpDelete]
        public IHttpActionResult UnlikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", commentId));

            // LOGGED USER
            var loggedUser = this.User.Identity.GetUserId();

            // LIKES
            var postCommentLikes = this.Data.CommentLikes.Where(cl => cl.Comment.Id == commentId);

            // LIKE TO REMOVE
            var likeToRemove = postCommentLikes.FirstOrDefault(l => l.Author.Id == loggedUser);

            // IF LIKE DOES NOT EXIST
            if (likeToRemove == null) return this.BadRequest("Comment is not liked.");

            // REMOVE LIKE AND SAVE
            this.Data.CommentLikes.Remove(likeToRemove);
            this.Data.SaveChanges();

            // COMMENT UNLIKE VOEW MODEL
            var likesViewModel = this.Data.Comments.Where(c => c.Id == commentId)
                .Select(UnlikedCommendViewModel.Create);

            return this.Ok(likesViewModel);
        }

        // COMMENT DETAILED LIKES
        [Route("api/posts/{postId}/comments/{commentId}/likes")]
        [HttpGet]
        public IHttpActionResult CommentLikesDetailed([FromUri] int postId, [FromUri] int commentId)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // IF COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", postId));

            // COMMENT LIKES
            var postCommentlikes = this.Data.CommentLikes.Where(cl => cl.Comment.Id == commentId)
                .Select(Models.ViewModels.Comment.CommentLikeViewModel.Create);

            return this.Ok(postCommentlikes);
        }

        // COMMENT PREVIEW LIKES
        [Route("api/posts/{postId}/comments/{commentId}/likes/preview")]
        [HttpGet]
        public IHttpActionResult CommentLikesPreview([FromUri] int postId, [FromUri] int commentId)
        {
            // POST
            var post = this.Data.Posts.Find(postId);

            // POST DOES NOT EXIST
            if (post == null) return this.BadRequest(InvalidEntity("post", postId));

            // COMMENT
            var postComment = post.Comments.FirstOrDefault(c => c.Id == commentId);

            // IF COMMENT DOES NOT EXIST
            if (postComment == null) return this.BadRequest(InvalidEntity("comment", postId));

            // COMMENT LIKES
            var postCommentLikesPreview = this.Data.Comments.Where(c => c.Id == commentId)
                .Take(10).Select(CommentLikePreviewViewModel.Create);

            return this.Ok(postCommentLikesPreview);
        }

        // POST/COMMENT DOES NOT EXIST MESSAGE
        private static string InvalidEntity(string entityName, int entityId)
        {
            var response = string.Format("{0} with id: {1} does not exist", entityName, entityId);

            return response;
        }
    }
}
