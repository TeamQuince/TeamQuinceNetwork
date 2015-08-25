namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<UserPost> userPosts;
        private ICollection<GroupPost> groupPosts;

        public User()
        {
            this.userPosts = new HashSet<UserPost>();
            this.groupPosts = new HashSet<GroupPost>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        public string WallPicture { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        public virtual ICollection<UserPost> UserPosts
        {
            get
            {
                return this.userPosts;
            }

            set
            {
                this.userPosts = value;
            }
        }

        public virtual ICollection<GroupPost> GroupPosts
        {
            get
            {
                return this.groupPosts;
            }

            set
            {
                this.groupPosts = value;
            }
        }
    }
}
