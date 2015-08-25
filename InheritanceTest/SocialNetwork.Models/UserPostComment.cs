namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserPostComment : Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserPostId { get; set; }

        public virtual UserPost UserPost { get; set; }
    }
}