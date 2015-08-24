namespace TQSN.Model
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
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
        private ICollection<Post> _posts;
        private ICollection<Group> _groups;

        public ApplicationUser()
        {
            this._posts = new HashSet<Post>();
            this._comments = new HashSet<Comment>();
            this._groups = new HashSet<Group>();
            this._likes = new HashSet<Like>();
        }

        // GROUPS - menberships
        public virtual ICollection<Group> Groups
        {
            get { return this._groups; }
            set { this._groups = value; }
        }


        // POSTS
        public virtual ICollection<Post> Posts
        {
            get { return this._posts; }
            set { this._posts = value; }
        }

        // COMMENTS
        public virtual ICollection<Comment> Comments
        {
            get { return this._comments; }
            set { this._comments = value; }
        }

        // LIKES
        public virtual ICollection<Like> Likes
        {
            get { return this._likes; }
            set { this._likes = value; }
        }
    }
}
