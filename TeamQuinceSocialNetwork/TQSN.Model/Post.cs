namespace TQSN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        private ICollection<Comment> _comments;
        private ICollection<Like> _likes;

        public Post()
        {
            this._comments = new HashSet<Comment>();
            this._likes = new HashSet<Like>();
        }

        // ID
        [Key]
        public int Id { get; set; }

        // CONTENT
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        // POSTED ON DATE
        [Required]
        public DateTime PostedOn { get; set; }

        // AUTHOR
        [Required]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        // POST LIKES
        public virtual ICollection<Like> Likes
        {
            get { return this._likes; }
            set { this._likes = value; }
        }

        // POST COMMENTS
        public virtual ICollection<Comment> Comments
        {
            get { return this._comments; }
            set { this._comments = value; }
        }

        // USER ID
        public string WallUserId { get; set; }
        public virtual ApplicationUser WallUser { get; set; }

        // GROUP ID
        public int? WallGroupId { get; set; }
        public virtual Group WallGroup { get; set; }
    }
    
}
