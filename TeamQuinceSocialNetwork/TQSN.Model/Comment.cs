namespace TQSN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        private ICollection<Like> _likes;

        public Comment()
        {
            this._likes = new HashSet<Like>();
        }        

        // ID
        [Key]
        public int Id { get; set; }

        // CONTENT
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        // COMMENTED ON DATE
        [Required]
        public DateTime PostedOn { get; set; }

        // AUTHOR
        [Required]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        // POST
        [Required]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        
        // COMMENT LIKES
        public virtual ICollection<Like> Likes
        {
            get { return this._likes; }
            set { this._likes = value; }
        }

    }
}
