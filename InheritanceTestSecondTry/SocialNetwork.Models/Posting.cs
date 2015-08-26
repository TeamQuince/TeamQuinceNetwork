namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Posting
    {
        private ICollection<Like> _likes;

        public Posting()
        {
            this._likes = new HashSet<Like>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        // POST/COMMENTS LIKES
        public virtual ICollection<Like> Likes
        {
            get { return this._likes; }
            set { this._likes = value; }
        }
    }
}
