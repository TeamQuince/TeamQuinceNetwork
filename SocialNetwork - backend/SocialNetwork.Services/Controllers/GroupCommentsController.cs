namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels.Comment;
    using SocialNetwork.Services.Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/groupposts")]
    public class GroupCommentsController : BaseApiController
    {
        [HttpPost]
        [Route("{postId}/comments")]
        public async Task<HttpResponseMessage> AddPostComment([FromUri] int postId, 
            [FromBody] AddCommentBindingModel model)
        {
            if (model == null)
            {
                return await this.BadRequest("Comment cannot be empty.").ExecuteAsync(new CancellationToken());
            }

            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var post = this.Data.GroupPosts.Find(postId);

            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!")
                    .ExecuteAsync(new CancellationToken());
            }

            if (!post.Owner.Members.Contains(currentUser) && currentUser != post.Owner.Owner)
            {
                return await this.BadRequest("Not allowed. You must be a member of the group.")
                    .ExecuteAsync(new CancellationToken());
            }

            var newComment = new Comment()
            {
                Content = model.CommentContent,
                Author = currentUser,
                PostedOn = DateTime.Now
            };

            post.Comments.Add(newComment);
            this.Data.SaveChanges();

            var commentPreview = this.Data.Comments
                .Where(p => p.Id == newComment.Id)
                .Select(AddCommentViewModel.Create)
                .FirstOrDefault();
            commentPreview.PostId = post.Id;

            return await this.Ok(commentPreview).ExecuteAsync(new CancellationToken());
        }

        [HttpPut]
        [Route("{postId}/comments/{commentId}")]
        public async Task<HttpResponseMessage> EditPostComment([FromUri] int postId, 
            [FromUri] int commentId,
            [FromBody] AddCommentBindingModel model)
        {
            if (model == null)
            {
                return await this.BadRequest("Comment cannot be empty.").ExecuteAsync(new CancellationToken());
            }

            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var post = this.Data.GroupPosts.Find(postId);

            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);

            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            
            if (comment.Author != currentUser)
            {
                return await this.BadRequest("Not allowed. User must be author of comment.")
                    .ExecuteAsync(new CancellationToken());
            }

            comment.Content = model.CommentContent;
            this.Data.SaveChanges();

            var commentPreview = this.Data.Comments
                .Where(p => p.Id == comment.Id)
                .Select(AddCommentViewModel.Create)
                .FirstOrDefault();
            commentPreview.PostId = post.Id;

            return await this.Ok(commentPreview).ExecuteAsync(new CancellationToken());
        }

        [HttpDelete]
        [Route("{postId}/comments/{commentId}")]
        public async Task<HttpResponseMessage> DeletePostComment([FromUri] int postId,
            [FromUri] int commentId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);
            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            if (!post.Comments.Contains(comment))
            {
                return await this.NotFound()
                    .ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            if (currentUser != comment.Author && currentUser != post.Owner.Owner)
            {
                return await this.BadRequest("Not allowed. User must be author of comment or group-owner.")
                    .ExecuteAsync(new CancellationToken());
            }

            var likes = comment.Likes.ToList();
            foreach (var like in likes)
            {
                this.Data.CommentLikes.Remove(like);
            }
            comment.Likes.Clear();
            post.Comments.Remove(comment);
            this.Data.Comments.Remove(comment);
            this.Data.SaveChanges();

            return await this.Ok().ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{postId}/comments")]
        public async Task<HttpResponseMessage> GetPostComments([FromUri] int postId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            var commentsPreviews = post.Comments
                .OrderByDescending(c => c.PostedOn)
                .AsQueryable()
                .Select(p => CommentViewModel.CreatePreview(currentUser, p))
                .ToList();

            return await this.Ok(commentsPreviews).ExecuteAsync(new CancellationToken());
        }

        [HttpPost]
        [Route("{postId}/comments/{commentId}/likes")]
        public async Task<HttpResponseMessage> LikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);
            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }
            if (!post.Comments.Contains(comment))
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!")
                    .ExecuteAsync(new CancellationToken());
            }

            if (comment.Likes.Any(l => l.Author == currentUser))
            {
                return await this.BadRequest("You have already liked this comment.")
                    .ExecuteAsync(new CancellationToken());
            }

            if (!post.Owner.Members.Contains(currentUser) && currentUser != post.Owner.Owner)
            {
                return await this.BadRequest("Not allowed. You must be member of the group.")
                    .ExecuteAsync(new CancellationToken());
            }

            var like = new CommentLike()
            {
                Author = currentUser
            };
            comment.Likes.Add(like);
            this.Data.SaveChanges();

            var commentPreview = this.Data.Comments
                .Where(c => c.Id == comment.Id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();
            commentPreview.Liked = true;

            return await this.Ok(commentPreview).ExecuteAsync(new CancellationToken());
        }

        [HttpDelete]
        [Route("{postId}/comments/{commentId}/likes")]
        public async Task<HttpResponseMessage> UnlikeComment([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);
            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }
            if (!post.Comments.Contains(comment))
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            var like = comment.Likes.FirstOrDefault(l => l.Author == currentUser);
            if (like == null)
            {
                return await this.BadRequest("You have not liked this comment.")
                    .ExecuteAsync(new CancellationToken());
            }
           
            comment.Likes.Remove(like);
            this.Data.CommentLikes.Remove(like);
            this.Data.SaveChanges();

            var commentPreview = this.Data.Comments
                .Where(c => c.Id == comment.Id)
                .Select(CommentViewModel.Create)
                .FirstOrDefault();
            commentPreview.Liked = false;

            return await this.Ok(commentPreview).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{postId}/comments/{commentId}/likes/preview")]
        public async Task<HttpResponseMessage> GetCommentPreviewLikes([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);
            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }
            if (!post.Comments.Contains(comment))
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var likesPreview = comment.Likes
                .Take(10)
                .Select(l => new { userId = l.Author.Id, commentId = comment.Id })
                .ToList();

            return await this.Ok(new { totalLikeCount = comment.Likes.Count, commentLikes = likesPreview })
                .ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{postId}/comments/{commentId}/likes")]
        public async Task<HttpResponseMessage> GetCommentDetaledLikes([FromUri] int postId, [FromUri] int commentId)
        {
            var post = this.Data.GroupPosts.Find(postId);
            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var comment = this.Data.Comments.Find(commentId);
            if (comment == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }
            if (!post.Comments.Contains(comment))
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var likes = comment.Likes
                .AsQueryable()
                .Select(SocialNetwork.Services.Models.ViewModels.CommentLikeViewModel.Create)
                .ToList();

            return await this.Ok(likes)
                .ExecuteAsync(new CancellationToken());
        }
    }
}