namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Linq;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class PreviewUserDataViewModel
    {
        public string id { get; set; }

        public string userName { get; set; }

        public string name { get; set; }

        public string profileImageData { get; set; }

        public string IsFriend { get; set; }

        public string hasPendingRequest { get; set; }

        public static PreviewUserDataViewModel GetPreviewUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new PreviewUserDataViewModel()
            {
                id = user.Id,
                userName = user.UserName,
                name = user.Name,
                IsFriend = user.Friends.Contains(currentUser).ToString(),
                profileImageData = user.ProfilePicture,
                hasPendingRequest = user.Requests
                    .Any(r => r.Sender == currentUser && r.Status == FriendRequestStatus.Pending)
                    .ToString()
            };
        }
    }
}