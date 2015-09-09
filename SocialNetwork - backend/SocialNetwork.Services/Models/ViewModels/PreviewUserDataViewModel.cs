namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Linq;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class PreviewUserDataViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ProfileImageData { get; set; }

        public string IsFriend { get; set; }

        public string HasPendingRequest { get; set; }

        public static PreviewUserDataViewModel GetPreviewUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new PreviewUserDataViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                IsFriend = user.Friends.Contains(currentUser).ToString().ToLower(),
                ProfileImageData = user.ProfilePicture,
                HasPendingRequest = user.Requests
                    .Any(r => r.Sender == currentUser && r.Status == FriendRequestStatus.Pending)
                    .ToString().ToLower()
            };
        }
    }
}