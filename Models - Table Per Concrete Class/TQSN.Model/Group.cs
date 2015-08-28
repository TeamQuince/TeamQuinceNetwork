namespace TQSN.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Group : WallPosts
    {
        
        private ICollection<ApplicationUser> _members;
        private ICollection<Post> _wallPosts;
      

        public Group()
        {
            this._members = new HashSet<ApplicationUser>();
            this._wallPosts = new List<Post>();
        }
	
        // ID
        [Key]
        public int Id { get; set; }

        //WALL
        public virtual ICollection<Post> Wall {
            get { return this._wallPosts; }
            set { this._wallPosts = value; }
        }
        public string Type { get { return "Group Wall"; }}
     


        // NAME
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        //DISCRIPTION 
        public string Discription { get; set; }

        //MEMBERS
        public virtual ICollection<ApplicationUser> Members {
            get { return this._members; }
            set { this._members = value; }
        }

    }
}
