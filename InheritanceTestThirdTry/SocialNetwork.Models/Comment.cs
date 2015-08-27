namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        private ICollection<CommentLike> likes;

        public Comment()
        {
            this.likes = new HashSet<CommentLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Post Post { get; set; }

        public virtual ICollection<CommentLike> Likes
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