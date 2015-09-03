namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class FriendshipRequestViewModel
    {
        public int Id { get; set; }
        public FriendRequestStatus Status { get; set; }
        public FriendViewModel Friend { get; set; }

        public FriendshipRequestViewModel()
        { }
        public FriendshipRequestViewModel(FriendshipRequest request)
        {
            this.Id = request.Id;
            this.Status = request.Status;
            this.Friend = new FriendViewModel(request.Recipient);
        }
        public static Expression<Func<FriendshipRequest, FriendshipRequestViewModel>> Create
        {
            get
            {
                return request => new FriendshipRequestViewModel()
                {
                    Id = request.Id,
                    Status = request.Status,
                    Friend = new FriendViewModel(request.Sender)
                };
            }
        }
    }
}