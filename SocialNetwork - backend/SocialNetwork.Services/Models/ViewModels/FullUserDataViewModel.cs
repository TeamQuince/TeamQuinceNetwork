namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Linq;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class FullUserDataViewModel
    {
        public string id { get; set; }

        public string userName { get; set; }

        public string name { get; set; }

        public string profileImageData { get; set; }

        public string coverImageData { get; set; }

        public string IsFriend { get; set; }

        public string hasPendingRequest { get; set; }

        public static FullUserDataViewModel GetFullUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new FullUserDataViewModel()
            {
                id = user.Id,
                userName = user.UserName,
                name = user.Name,
                profileImageData = user.ProfilePicture,
                coverImageData = user.WallPicture,
                IsFriend = user.Friends.Contains(currentUser).ToString(),
                hasPendingRequest = user.Requests
                    .Any(r => r.Sender == currentUser && r.Status == FriendRequestStatus.Pending)
                    .ToString()
            };
        }
    }
}