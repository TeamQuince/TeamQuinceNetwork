namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Threading.Tasks;

    using SocialNetwork.Models.Enumerations;

    public class ApplicationUser : IdentityUser
    {
        private ICollection<Post> posts;
        private ICollection<Comment> comments;
        private ICollection<Group> groups;
        private ICollection<ApplicationUser> friends;
        private ICollection<FriendshipRequest> requests;
        private Wall wall;

        public ApplicationUser()
        {
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
            this.groups = new HashSet<Group>();
            this.friends = new HashSet<ApplicationUser>();
            this.requests = new HashSet<FriendshipRequest>();
            this.wall = new Wall();
        }

        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public UserGender Gender { get; set; }

        public string ProfilePicture { get; set; }

        public string WallPicture { get; set; }

        public virtual ICollection<Post> Posts
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

        public virtual ICollection<Group> Groups
        {
            get
            {
                return this.groups;
            }

            set
            {
                this.groups = value;
            }
        }

        public virtual ICollection<ApplicationUser> Friends
        {
            get
            {
                return this.friends;
            }

            set
            {
                this.friends = value;
            }
        }

        public virtual ICollection<FriendshipRequest> Requests
        {
            get
            {
                return this.requests;
            }

            set
            {
                this.requests = value;
            }
        }

        public virtual Wall Wall
        {
            get
            {
                return this.wall;
            }

            set
            {
                this.wall = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}