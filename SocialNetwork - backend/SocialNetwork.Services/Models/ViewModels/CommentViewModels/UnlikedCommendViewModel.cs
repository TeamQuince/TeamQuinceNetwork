namespace SocialNetwork.Services.Models.ViewModels.CommentViewModels
{
    using System;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class UnlikedCommendViewModel
    {
        public int CommentId { get; set; }

        public int LikesCount { get; set; }

        public bool Liked { get; set; }

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