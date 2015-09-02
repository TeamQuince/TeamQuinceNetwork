namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class DataAboutMeViewModel
    {
        public string userName { get; set; }

        public string name { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string profileImage { get; set; }

        public int friendsCount { get; set; }

        public static Expression<Func<ApplicationUser, DataAboutMeViewModel>> Create
        {
            get
            {
                return user => new DataAboutMeViewModel()
                {
                    userName = user.UserName,
                    name = user.Name,
                    gender = user.Gender.ToString(),
                    email = user.Email,
                    profileImage = user.ProfilePicture,
                    friendsCount = user.Friends.Count
                };
            }
        }
    }
}