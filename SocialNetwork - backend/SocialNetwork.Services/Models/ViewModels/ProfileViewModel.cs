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
        public string Username { get; set; }

        public string Name { get; set; }

        public UserGender Gender { get; set; }

        public string Email { get; set; }

        public string ProfilePicture { get; set; }

        public int FriendsCount { get; set; }
        public ProfileViewModel(ApplicationUser appUser)
        {
            Username = appUser.UserName;
            Name = appUser.Name;
            Gender = appUser.Gender;
            Email = appUser.Email;
            ProfilePicture = appUser.ProfilePicture;
            FriendsCount = appUser.Friends.Count;
        }
    }
}