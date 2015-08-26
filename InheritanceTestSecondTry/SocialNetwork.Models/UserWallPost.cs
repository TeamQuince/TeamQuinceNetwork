using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class UserWallPost : Posting
    {
        private ICollection<UserWallPostComment> _comments;

        public UserWallPost()
        {
            this._comments = new HashSet<UserWallPostComment>();
        }

        [Required]
        public int WallOwnerId { get; set; }
        public virtual User WallOwner { get; set; }

        public virtual ICollection<UserWallPostComment> Comments
        {
            get { return this._comments; }
            set { this._comments = value; }
        }
    }
}
