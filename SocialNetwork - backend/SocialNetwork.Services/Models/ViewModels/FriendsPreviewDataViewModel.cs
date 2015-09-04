namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using SocialNetwork.Models;

    public class FriendsPreviewDataViewModel
    {
        public int TotalCount { get; set; }

        public IEnumerable<PreviewUserDataViewModel> Friends { get; set; }

        public static object Create(ApplicationUser user)
        {
            return new
            {
                TotalCount = user.Friends.Count,
                Friends = user.Friends
                    .Take(6)
                    .Select(u => new
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