namespace SocialNetwork.Services.Models.ViewModels.Comment
{
    using System;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class UnlikedCommendViewModel
    {
        // COMMENT ID
        public int CommentId { get; set; }

        // LIKES COUNT
        public int LikesCount { get; set; }

        // LIKE ID
        public bool Liked { get; set; }

        // CREATE
        public static Expression<Func<Comment, UnlikedCommendViewModel>> Create
        {
            get
            {
                return comment => new UnlikedCommendViewModel
                {
                    CommentId = comment.Id,
                    LikesCount = comment.Likes.Count,
                    Liked = false
                };
            }
        }
    }
}