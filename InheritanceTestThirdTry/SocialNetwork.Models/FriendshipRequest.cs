namespace SocialNetwork.Models
{
    using SocialNetwork.Models.Enumerations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FriendshipRequest
    {
        private FriendRequestStatus status;

        public FriendshipRequest()
        {
            this.status = FriendRequestStatus.Pending;
        }

        [Key]
        public int Id { get; set; }

        public FriendRequestStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }

        public virtual User Sender { get; set; }

        public virtual User Recipient { get; set; }
    }
}
