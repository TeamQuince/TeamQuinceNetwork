namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserPostCommentLike : Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserPostCommentId { get; set; }

        public virtual UserPostComment Comment { get; set; }
    }
}