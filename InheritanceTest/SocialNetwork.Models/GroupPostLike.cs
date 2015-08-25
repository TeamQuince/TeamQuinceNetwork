namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GroupPostLike : Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupPostId { get; set; }

        public virtual GroupPost Post { get; set; }
    }
}