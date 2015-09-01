namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class LikeViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int PostId { get; set; }

        public string ProfileImage { get; set; }

        public static Expression<Func<PostLike, LikeViewModel>> Create
        {
            get
            {
                return like => new LikeViewModel()
                {
                    UserId = like.Author.Id,
                    Name = like.Author.Name,
                    Username = like.Author.UserName,
                    PostId = like.Post.Id,
                    ProfileImage = like.Author.ProfilePicture
                };
            }
        }
    }
}