namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostsController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> AddPostToUser(AddPostBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            if (model == null)
            {
                return await this.BadRequest("Post cannot be empty.").ExecuteAsync(new CancellationToken());
            }

            var user = this.Data.Users.FirstOrDefault(u => u.UserName == model.username);
            if (user == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            //if (!user.Friends.Contains(currentUser))
            //{
            //    return await this.BadRequest("Not allowed. Wall-owner must be a friend or user.").ExecuteAsync(new CancellationToken());
            //}

            var newPost = new Post()
            {
                Content = model.postContent,
                Author = currentUser,
                PostedOn = DateTime.Now
            };

            user.Posts.Add(newPost);
            this.Data.SaveChanges();

            var dbPost = this.Data.Posts.FirstOrDefault(p => p.Id == newPost.Id);
            var postReturnView = new PostViewModel()
            {
                Id = dbPost.Id,
                AuthorId = dbPost.Author.Id,
                AuthorUsername = dbPost.Author.UserName,
                AuthorProfileImage = dbPost.Author.ProfilePicture,
                WallOwnerId = user.Id,
                PostContent = dbPost.Content,
                Date = dbPost.PostedOn,
                LikesCount = dbPost.Likes.Count,
                Liked = false,
                TotalCommentsCount = 0,
                Comments = new List<CommentViewModel>()
            };

            return await this.Ok(new
            {
                message = "Post successfully added.",
                post = postReturnView
            }).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetPostById([FromUri] int id)
        {
            var post = this.Data.Posts.FirstOrDefault(p => p.Id == id);

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

            var postPreview = PostViewModel.CreatePostPreview(currentUser, post);

            return await this.Ok(postPreview).ExecuteAsync(new CancellationToken());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeletePost([FromUri] int id)
        {
            var post = this.Data.Posts.FirstOrDefault(p => p.Id == id);

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

            if (post.Author != currentUser && currentUser != post.Owner)
            {
                return await this.Unauthorized().ExecuteAsync(new CancellationToken());
            }

            // Now delete the post: 1) Delete all likes of all post comments, 2) Delete all comments, 
            // 3) Delete all post likes, 4) Delete post
            var postComments = post.Comments.ToList();
            foreach (var comment in postComments)
            {
                var likes = comment.Likes.ToList();
                foreach (var like in likes)
                {
                    this.Data.CommentLikes.Remove(like);
                }
                comment.Likes.Clear();
                post.Comments.Remove(comment);
                this.Data.Comments.Remove(comment);
            }

            var postLikes = post.Likes.ToList();
            foreach (var like in postLikes)
            {
                this.Data.PostLikes.Remove(like);
            }
            post.Likes.Clear();

            this.Data.Posts.Remove(post);
            this.Data.SaveChanges();

            return await this.Ok().ExecuteAsync(new CancellationToken());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> EditPostById([FromUri] int id, [FromBody] EditPostBindingModel model) 
        {
            if (model == null)
            {
                return await this.BadRequest("Post cannot be empty.").ExecuteAsync(new CancellationToken());
            }

            if (!this.ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var post = this.Data.Posts.FirstOrDefault(p => p.Id == id);

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

            if (post.Author != currentUser)
            {
                return await this.Unauthorized().ExecuteAsync(new CancellationToken());
            }

            post.Content = model.postContent;
            this.Data.SaveChanges();

            var dbPost = this.Data.Posts.Where(p => p.Id == post.Id)
                .Select(PostViewModel.Create)
                .FirstOrDefault();

            return await this.Ok(dbPost).ExecuteAsync(new CancellationToken());
        }

        [HttpPost]
        [Route("{postId}/likes")]
        public async Task<HttpResponseMessage> LikePostById([FromUri] int postId)
        {
            var post = this.Data.Posts.Find(postId);

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

            if (!currentUser.Friends.Contains(post.Author) && 
                !currentUser.Friends.Contains(post.Owner) &&
                currentUser != post.Author)
            {
                return await this.BadRequest("Not allowed. Post author and/or wall-owner must be a friend.")
                    .ExecuteAsync(new CancellationToken());
            }

            var like = post.Likes.FirstOrDefault(l => l.Author == currentUser);
            if (like != null)
            {
                return await this.BadRequest("Post is already liked by user.")
                    .ExecuteAsync(new CancellationToken());
            }

            post.Likes.Add(new PostLike()
            {
                Author = currentUser
            });
            this.Data.SaveChanges();
            var dbPost = this.Data.Posts.Find(post.Id);

            return await this.Ok(new { postId = dbPost.Id, likesCount = dbPost.Likes.Count, liked = "true" })
                .ExecuteAsync(new CancellationToken());
        }

        [HttpDelete]
        [Route("{postId}/likes")]
        public async Task<HttpResponseMessage> UnlikePostById([FromUri] int postId)
        {
            var post = this.Data.Posts.Find(postId);

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

            if (!currentUser.Friends.Contains(post.Author) &&
                !currentUser.Friends.Contains(post.Owner) &&
                currentUser != post.Author)
            {
                return await this.BadRequest("Not allowed. Post author and/or wall-owner must be a friend.")
                    .ExecuteAsync(new CancellationToken());
            }

            var like = post.Likes.FirstOrDefault(l => l.Author == currentUser);
            if (like == null)
            {
                return await this.BadRequest("Not allowed. You must like the post first.")
                    .ExecuteAsync(new CancellationToken());
            }

            post.Likes.Remove(like);
            this.Data.SaveChanges();

            var dbPost = this.Data.Posts.Find(post.Id);
            return await this.Ok(new { postId = dbPost.Id, likesCount = dbPost.Likes.Count, liked = "false" })
                .ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{postId}/likes/preview")]
        public async Task<HttpResponseMessage> GetPostPreviewLikes([FromUri] int postId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var likesPreview = post.Likes
                .Take(10)
                .Select(l => new { userId = l.Author.Id, postId = post.Id })
                .ToList();

            return await this.Ok(new { totalLikeCount = post.Likes.Count, postLikes = likesPreview })
                .ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("{postId}/likes")]
        public async Task<HttpResponseMessage> GetPostDetailedLikes([FromUri] int postId)
        {
            var post = this.Data.Posts.Find(postId);

            if (post == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var likes = post.Likes
                .AsQueryable()
                .Select(LikeViewModel.Create)
                .ToList();

            return await this.Ok(likes)
                .ExecuteAsync(new CancellationToken());
        }
    }
}