namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserPost : Post
    {
        private ICollection<UserPostComment> comments;

        public UserPost()
        {
            this.comments = new HashSet<UserPostComment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int TargetUserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<UserPostComment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}
