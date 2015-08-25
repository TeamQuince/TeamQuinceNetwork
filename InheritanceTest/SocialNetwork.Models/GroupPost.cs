namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupPost : Post
    {
        private ICollection<GroupPostComment> comments;

        public GroupPost()
        {
            this.comments = new HashSet<GroupPostComment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int TargetGroupId { get; set; }

        public virtual Group Group { get; set; }

        public ICollection<GroupPostComment> Comments
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
