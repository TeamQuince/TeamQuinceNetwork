namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using SocialNetwork.Models;

    public class FriendsFullDataViewModel
    {
        public int totalCount { get; set; }

        public IEnumerable<PreviewUserDataViewModel> friends { get; set; }

        public static object Create(ApplicationUser user)
        {
            return new
            {
                totalCount = user.Friends.Count,
                friends = user.Friends.Select(u => new
                    {
                        id = u.Id,
                        userName = u.UserName,
                        name = u.Name,
                        image = u.ProfilePicture
                    })
            };
        }
    }
}