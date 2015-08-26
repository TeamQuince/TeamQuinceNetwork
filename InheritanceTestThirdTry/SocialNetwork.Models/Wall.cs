namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Wall
    {
        private ICollection<Post> posts;

        public Wall()
        {
            this.posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        public ICollection<Post> Posts
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
    }
}
