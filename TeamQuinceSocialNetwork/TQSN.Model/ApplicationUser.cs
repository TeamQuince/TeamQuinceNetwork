namespace TQSN.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, WallPosts

    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            
            // Add custom user claims here
            return userIdentity;
        }

        private ICollection<Comment> _comments;
        private ICollection<Like> _likes;
        private ICollection<Post> _wallPosts;
        private ICollection<Post> _posts;
        private ICollection<Group> _groups;
        private ICollection<ApplicationUser> _friends;
        private ICollection<FriendshipRequest> _friendshipRequestsAuthor;
        private ICollection<FriendshipRequest> _friendshipRequestsRecipient;

        public ApplicationUser()
        {
           this._posts = new HashSet<Post>();
           this._wallPosts = new List<Post>();
           this._comments = new List<Comment>();
           this._groups = new HashSet<Group>();
           this._likes = new HashSet<Like>();
           this._friends = new HashSet<ApplicationUser>();
           this._friendshipRequestsAuthor = new List<FriendshipRequest>();
           this._friendshipRequestsRecipient = new List<FriendshipRequest>();
        }
        //WALL
        public virtual ICollection<Post> Wall
        {
            get { return this._wallPosts; }
            set { this._wallPosts = value; }
        }
        public string Type {get { return "User Wall"; }}
       


        // GROUPS - menberships
        public virtual ICollection<Group> Groups
        {
            get { return this._groups; } 
            set { this._groups = value; }
        }

        //POSTS AUTHORED
        public virtual ICollection<Post> PostsAuthored {
            get { return this._posts; }
            set { this._posts = value; }
        }

        //COMMENT AUTHORED
        public virtual ICollection<Comment> CommentAuthoredCollection
        {
            get { return this._comments; }
            set { this._comments = value; }
        }


        // LIKES AUTHORED
        public virtual ICollection<Like> LikesAuthoredCollection
        {
            get { return this._likes; }
            set { this._likes = value; }
        }


        // FRIENDS
        [ForeignKey("Friends")]
        public string FriendId { get; set; }
        public virtual ICollection<ApplicationUser> Friends
        {
            get { return this._friends; }
            set { this._friends = value; }
        }

        // FRIENDSHIP REQUESTS

        public virtual ICollection<FriendshipRequest> FriendshipRequestsAuthor
        {
            get { return this._friendshipRequestsAuthor; }
            set { this._friendshipRequestsAuthor = value; }
        }

        public virtual ICollection<FriendshipRequest> FriendshipRequestsRecipient
        {
            get { return this._friendshipRequestsRecipient; }
            set { this._friendshipRequestsRecipient = value; }
        }

       
    }
}
