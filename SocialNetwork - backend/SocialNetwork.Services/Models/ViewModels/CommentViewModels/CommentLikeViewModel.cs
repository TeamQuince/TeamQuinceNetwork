namespace SocialNetwork.Services.Models.ViewModels.CommentViewModels
{
    using System;
    using System.Linq.Expressions;
    using SocialNetwork.Models;

    public class CommentLikeViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int PostId { get; set; }

        public string ProfileImage { get; set; }

        public static Expression<Func<CommentLike, CommentLikeViewModel>> Create
        {
            get
            {
                return like => new CommentLikeViewModel
                {
                    UserId = like.Author.Id,
                    Name = like.Author.Name,
                    Username = like.Author.UserName,
                    PostId = like.Comment.Id,
                    ProfileImage = like.Author.ProfilePicture
                };
            }
        }
    }
}