namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserPostComment : Posting
    {
        private ICollection<UserPostCommentLike> likes;

        public UserPostComment()
        {
            this.likes = new HashSet<UserPostCommentLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserPostId { get; set; }

        public virtual UserPost UserPost { get; set; }

        public ICollection<UserPostCommentLike> Likes 
        {
            get
            {
                return this.likes;
            }

            set
            {
                this.likes = value;
            }
        }
    }
}