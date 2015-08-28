namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupPostComment : Posting
    {
        private ICollection<GroupPostCommentLike> likes;

        public GroupPostComment()
        {
            this.likes = new HashSet<GroupPostCommentLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupPostId { get; set; }

        public virtual GroupPost GroupPost { get; set; }

        public virtual ICollection<GroupPostCommentLike> Likes 
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