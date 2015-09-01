namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        private ICollection<ApplicationUser> members;
        private ICollection<GroupPost> posts;

        public Group()
        {
            this.members = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string WallPicture { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<ApplicationUser> Members 
        {
            get
            {
                return this.members;
            }

            set
            {
                this.members = value;
            }
        }

        public virtual ICollection<GroupPost> Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
            }
        }
    }
}
