namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Linq;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class FullUserDataViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ProfileImageData { get; set; }

        public string CoverImageData { get; set; }

        public string IsFriend { get; set; }

        public string HasPendingRequest { get; set; }

        public static FullUserDataViewModel GetFullUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new FullUserDataViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                ProfileImageData = user.ProfilePicture,
                CoverImageData = user.WallPicture,
                IsFriend = user.Friends.Contains(currentUser).ToString(),
                HasPendingRequest = user.Requests
                    .Any(r => r.Sender == currentUser && r.Status == FriendRequestStatus.Pending)
                    .ToString()
            };
        }
    }
}