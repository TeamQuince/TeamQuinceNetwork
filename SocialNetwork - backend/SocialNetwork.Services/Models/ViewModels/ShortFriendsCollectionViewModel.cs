namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using SocialNetwork.Models;

    public class ShortFriendsCollectionViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<PreviewUserDataViewModel> Friends { get; set; }

        public static ShortFriendsCollectionViewModel Create(ApplicationUser user)
        {
            return new ShortFriendsCollectionViewModel()
            {
                TotalCount = user.Friends.Count,
                Friends = user.Friends
                    .Take(6)
                    .Select(u => new PreviewUserDataViewModel()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        ProfileImage = u.ProfilePicture
                    })

            };

        }
    }
}