namespace SocialNetwork.Services.Models.ViewModels.Comment
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class CommentLikePreviewViewModel
    {
        public int TotalLikesCount { get; set; }

        public IEnumerable CommentLikes { get; set; }

        public static Expression<Func<Comment, CommentLikePreviewViewModel>> Create
        {
            get
            {
                return comment => new CommentLikePreviewViewModel
                {
                    TotalLikesCount = comment.Likes.Count,
                    CommentLikes = comment.Likes.Select(l => new
                    {
                        userId = comment.Author.Id,
                        commentId = comment.Id
                    })
                };
            }
        }
    }
}