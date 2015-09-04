namespace SocialNetwork.Services.Models.ViewModels.CommentViewModels
{
    using System;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class LikedCommentViewModel
    {
        public int CommentId { get; set; }

        public int LikesCount { get; set; }

        public bool Liked { get; set; }

        public static Expression<Func<Comment, LikedCommentViewModel>> Create
        {
            get
            {
                return comment => new LikedCommentViewModel
                {
                    CommentId = comment.Id,
                    LikesCount = comment.Likes.Count,
                    Liked = true
                };
            }
        }
    }
}