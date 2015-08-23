namespace TQSN.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        private ICollection<ApplicationUser> _members;
        private ICollection<Post> _posts;

        public Group()
        {
            this._members = new HashSet<ApplicationUser>();
            this._posts = new HashSet<Post>();
        }
	
        // ID
        [Key]
        public int Id { get; set; }

        // NAME
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        // OWNER
        [Required]
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<ApplicationUser> Comments
        {
            get { return this._members; }
            set { this._members = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return this._posts; }
            set { this._posts = value; }
        }


    }
}
