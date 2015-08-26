namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<Post> posts;
        private ICollection<Comment> comments;
        private ICollection<Group> groups;

        public User()
        {
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
            this.groups = new HashSet<Group>();
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

        public virtual ICollection<Post> Posts
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

        public virtual ICollection<Comment> Comments
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

        public virtual ICollection<Group> Groups
        {
            get
            {
                return this.groups;
            }

            set
            {
                this.groups = value;
            }
        }
    }
}
