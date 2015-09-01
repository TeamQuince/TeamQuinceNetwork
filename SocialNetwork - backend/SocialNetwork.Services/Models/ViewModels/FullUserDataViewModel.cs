namespace SocialNetwork.Services.Models.ViewModels
{
    using Microsoft.Ajax.Utilities;
    using SocialNetwork.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    public class FullUserDataViewModel
    {
        public string Id;
        public string userName;
        public string name;
        public string profileImage;
        public bool isFriend;

        public FullUserDataViewModel(ApplicationUser user, ApplicationUser currentUser)
        {
            this.Id = user.Id;
            this.userName = user.UserName;
            this.name = user.Name;
            this.profileImage = user.ProfilePicture;
            this.isFriend = user.Friends.Contains(currentUser);
        }
    }
}