namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserPostLike : Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserPostId { get; set; }

        public virtual UserPost Post { get; set; }
    }
}