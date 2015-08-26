namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Post
    {
        private ICollection<Comment> comments;
        private ICollection<PostLike> likes;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<PostLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public virtual User Author { get; set; }

        public virtual Wall Wall { get; set; }

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

        public virtual ICollection<PostLike> Likes
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
