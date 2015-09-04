namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class DataAboutMeViewModel
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string ProfileImageData { get; set; }

        public string CoverImageData { get; set; }

        public int FriendsCount { get; set; }

        public static Expression<Func<ApplicationUser, DataAboutMeViewModel>> Create
        {
            get
            {
                return user => new DataAboutMeViewModel()
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    Gender = (int)user.Gender,
                    Email = user.Email,
                    ProfileImageData = user.ProfilePicture,
                    CoverImageData = user.WallPicture,
                    FriendsCount = user.Friends.Count
                };
            }
        }
    }
}