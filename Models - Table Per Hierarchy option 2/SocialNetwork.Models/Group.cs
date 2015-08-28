namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        private ICollection<User> members;
        private ICollection<Posting> postings;

        public Group()
        {
            this.members = new HashSet<User>();
            this.postings = new HashSet<Posting>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Members 
        {
            get
            {
                return this.members;
            }

            set
            {
                this.members = value;
            }
        }

        public virtual ICollection<Posting> Postings
        {
            get
            {
                return this.postings;
            }

            set
            {
                this.postings = value;
            }
        }
    }
}
