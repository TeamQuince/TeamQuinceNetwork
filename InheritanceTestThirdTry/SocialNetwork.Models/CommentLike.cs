namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CommentLike
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Comment Comment { get; set; }
    }
}