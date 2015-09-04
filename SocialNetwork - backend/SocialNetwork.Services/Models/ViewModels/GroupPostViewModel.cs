namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class GroupPostViewModel
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string AuthorProfileImage { get; set; }

        public string WallOwnerId { get; set; }

        public string PostContent { get; set; }

        public DateTime Date { get; set; }

        public int LikesCount { get; set; }

        public bool Liked { get; set; }

        public int TotalCommentsCount { get; set; }

        public IList<CommentViewModel> Comments { get; set; }

        public static Expression<Func<GroupPost, GroupPostViewModel>> Create
        {
            get
            {
                return post => new GroupPostViewModel()
                {
                    Id = post.Id,
                    AuthorId = post.Author.Id,
                    AuthorUsername = post.Author.UserName,
                    AuthorProfileImage = post.Author.ProfilePicture,
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
    }
}