namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class CommentLikeViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int CommentId { get; set; }

        public string ProfileImageData { get; set; }

        public static Expression<Func<CommentLike, CommentLikeViewModel>> Create
        {
            get
            {
                return like => new CommentLikeViewModel()
                {
                    UserId = like.Author.Id,
                    Name = like.Author.Name,
                    Username = like.Author.UserName,
                    CommentId = like.Comment.Id,
                    ProfileImageData = like.Author.ProfilePicture
                };
            }
        }
    }
}