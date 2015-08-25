namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GroupPostCommentLike : Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupPostCommentId { get; set; }

        public virtual GroupPostComment Comment { get; set; }
    }
}