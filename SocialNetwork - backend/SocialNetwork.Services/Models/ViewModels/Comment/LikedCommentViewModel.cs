namespace SocialNetwork.Services.Models.ViewModels.Comment
{
    using System;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class LikedCommentViewModel
    {
        // COMMENT ID
        public int CommentId { get; set; }

        // LIKES COUNT
        public int LikesCount { get; set; }

        // LIKED
        public bool Liked { get; set; }

        // CREATE
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