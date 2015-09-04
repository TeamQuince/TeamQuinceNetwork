namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using SocialNetwork.Models;

    public class FriendsFullDataViewModel
    {
        public int TotalCount { get; set; }

        public IEnumerable<PreviewUserDataViewModel> Friends { get; set; }

        public static object Create(ApplicationUser user)
        {
            return new
            {
                totalCount = user.Friends.Count,
                friends = user.Friends.Select(u => new
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Name = u.Name,
                        Image = u.ProfilePicture
                    })
            };
        }
    }
}