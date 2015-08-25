namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupPost : Posting
    {
        private ICollection<GroupPostComment> comments;
        private ICollection<GroupPostLike> likes;

        public GroupPost()
        {
            this.comments = new HashSet<GroupPostComment>();
            this.likes = new HashSet<GroupPostLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<GroupPostComment> Comments
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

        public virtual ICollection<GroupPostLike> Likes
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
