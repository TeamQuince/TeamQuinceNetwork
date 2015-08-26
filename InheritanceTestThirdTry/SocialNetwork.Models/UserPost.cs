namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserPost : Posting
    {
        private ICollection<UserPostComment> comments;
        private ICollection<UserPostLike> likes;

        public UserPost()
        {
            this.comments = new HashSet<UserPostComment>();
            this.likes = new HashSet<UserPostLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

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

        public virtual ICollection<UserPostLike> Likes
        {
            get
            {
                return this.likes;
            }
            set
            {
                this.likes = value;
            }
        }
    }
}
