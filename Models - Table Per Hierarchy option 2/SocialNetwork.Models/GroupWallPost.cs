namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupWallPost : Posting
    {
        
        private ICollection<Posting> _comments;
        
        public GroupWallPost()
        {
            this._comments = new HashSet<Posting>();
        }

        [Required]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Posting> Comments
        {
            get { return this._comments; }
            set { this._comments = value; }
        }
    }
}
