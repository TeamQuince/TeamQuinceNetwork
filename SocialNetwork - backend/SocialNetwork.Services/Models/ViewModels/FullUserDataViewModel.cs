﻿namespace SocialNetwork.Services.Models.ViewModels
{
    using SocialNetwork.Models;

    public class FullUserDataViewModel
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public bool IsFriend { get; set; }
        //  public UserGender Gender { get; set; }
        //   public string WallPicture { get; set; }
        //  public bool HasPendingRequest { get; set; }
        //  public List<Group> Groups { get; set; }

        public static FullUserDataViewModel GetFullUserData(ApplicationUser user, ApplicationUser currentUser)
        {
            return new FullUserDataViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                ProfileImage = user.ProfilePicture,
                //Gener = user.Gender,
                //WallPicture = user.WallPicture,
                //HasPendingRequest = user.Requests
                //    .Any(r => r.Status == FriendRequestStatus.Pending && 
                //        (r.Sender.Id == currentUser.Id || r.Recipient.Id == currentUser.Id)),
                //GROUPS TO DO WITH GROUPS VIEW MODEL
                IsFriend = user.Friends.Contains(currentUser),
            };
        }
    }
}