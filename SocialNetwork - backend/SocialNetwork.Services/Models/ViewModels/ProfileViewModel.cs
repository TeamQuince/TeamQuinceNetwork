namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class ProfileViewModel
    {
        public string userName { get; set; }

        public string name { get; set; }

        public int gender { get; set; }

        public string email { get; set; }

        public string profileImageData { get; set; }

        public string coverImageData { get; set; }

        public int friendsCount { get; set; }

        public ProfileViewModel(ApplicationUser appUser)
        {
            userName = appUser.UserName;
            name = appUser.Name;
            gender = (int)appUser.Gender;
            email = appUser.Email;
            profileImageData = appUser.ProfilePicture;
            coverImageData = appUser.WallPicture;
            friendsCount = appUser.Friends.Count;
        }
    }
}