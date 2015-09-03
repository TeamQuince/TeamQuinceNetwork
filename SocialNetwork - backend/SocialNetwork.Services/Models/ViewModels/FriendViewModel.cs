namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    using SocialNetwork.Models;
    public class FriendViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public FriendViewModel()
        { }

        public FriendViewModel(ApplicationUser friend)
        {
            this.Id = friend.Id;
            this.Username = friend.UserName;
            this.Name = friend.Name;
            this.Image = friend.ProfilePicture;
        }
        public static Expression<Func<ApplicationUser, FriendViewModel>> Create
        {
            get
            {
                return friend => new FriendViewModel()
                {
                    Id = friend.Id,
                    Username = friend.UserName,
                    Name = friend.Name,
                    Image = friend.ProfilePicture
                };
            }
        }
    }
}