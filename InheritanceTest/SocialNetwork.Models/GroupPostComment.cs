namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GroupPostComment : Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupPostId { get; set; }

        public virtual GroupPost GroupPost { get; set; }
    }
}