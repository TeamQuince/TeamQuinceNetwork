namespace SocialNetwork.Services.Models.ViewModels
{
    using SocialNetwork.Models;

    public class PreviewUserDataViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        //   public string Name { get; set; }
        public string ProfileImage { get; set; }
        //   public bool IsFriend { get; set; }
        //  public UserGender Gender { get; set; }
        //   public string WallPicture { get; set; }
        //  public bool HasPendingRequest { get; set; }
        //  public List<Group> Groups { get; set; }

        public static PreviewUserDataViewModel GetPreviewUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new PreviewUserDataViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                //  Name = user.Name,
                //  IsFriend = user.Friends.Contains(currentUser),
                ProfileImage = user.ProfilePicture,
                //Gener = user.Gender,
                //WallPicture = user.WallPicture,
                //HasPendingRequest = user.Requests
                //    .Any(r => r.Status == FriendRequestStatus.Pending && 
                //        (r.Sender.Id == currentUser.Id || r.Recipient.Id == currentUser.Id)),
                //GROUPS TO DO WITH GROUPS VIEW MODEL

            };
        }
    }
}