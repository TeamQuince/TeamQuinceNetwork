namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Comment;
    using SocialNetwork.Models;

    public class PostViewModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string AuthorProfileImage { get; set; }

        public string WallOwnerId { get; set; }

        public string  PostContent { get; set; }

        public DateTime Date { get; set; }

        public int LikesCount { get; set; }

        public bool Liked { get; set; }

        public int TotalCommentsCount { get; set; }

        public IList<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Post, PostViewModel>> Create
        {
            get
            {
                return post => new PostViewModel()
                {
                    Id = post.Id,
                    AuthorId = post.Author.Id,
                    AuthorUsername = post.Author.UserName,
                    AuthorProfileImage = post.Author.ProfilePicture,
                    WallOwnerId = post.Owner.Id,
                    PostContent = post.Content,
                    Date = post.PostedOn,
                    LikesCount = post.Likes.Count,
                    TotalCommentsCount = post.Comments.Count,
                    Comments = post.Comments
                        .OrderByDescending(c => c.PostedOn)
                        .AsQueryable()
                        .Select(CommentViewModel.Create).ToList()
                };
            }
        }

        public static object CreatePostPreview(ApplicationUser currentUser, Post post)
        {
            return new 
            {
                Id = post.Id,
                AuthorId = post.Author.Id,
                AuthorUsername = post.Author.UserName,
                AuthorProfileImage = post.Author.ProfilePicture,
                WallOwnerId = post.Owner.Id,
                PostContent = post.Content,
                Date = post.PostedOn,
                LikesCount = post.Likes.Count,
                Liked = post.Likes.Any(l => l.Author == currentUser),
                TotalCommentsCount = post.Comments.Count,
                Comments = post.Comments
                    .OrderByDescending(c => c.PostedOn)
                    .AsQueryable()
                    //.Select(c => CommentViewModel.CreatePreview(currentUser, c)).ToList()
                    .Select(CommentLikePreviewViewModel.Create)
            };
        }

        public static object CreateGroupPostPreview(ApplicationUser currentUser, GroupPost post)
        {
            return new
            {
                Id = post.Id,
                AuthorId = post.Author.Id,
                AuthorUsername = post.Author.UserName,
                AuthorProfileImage = post.Author.ProfilePicture,
                WallOwnerId = post.Owner.Id,
                PostContent = post.Content,
                Date = post.PostedOn,
                LikesCount = post.Likes.Count,
                Liked = post.Likes.Any(l => l.Author == currentUser),
                TotalCommentsCount = post.Comments.Count,
                Comments = post.Comments
                    .OrderByDescending(c => c.PostedOn)
                    .AsQueryable()
                    //.Select(c=> CommentViewModel.CreatePreview(currentUser, c)).ToList()
                    .Select(CommentLikePreviewViewModel.Create)
            };
        }
    }
}