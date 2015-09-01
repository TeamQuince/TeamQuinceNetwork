namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SocialNetwork.Models;

    public class FriendsCollectionViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<PreviewUserDataViewModel> Friends { get; set; }

        public static FriendsCollectionViewModel Create(ApplicationUser user)
        {
            return new FriendsCollectionViewModel()
            {
                TotalCount = user.Friends.Count,
                Friends = user.Friends.Select(u => new PreviewUserDataViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    ProfileImage = u.ProfilePicture
                })
            };

        }
    }
}