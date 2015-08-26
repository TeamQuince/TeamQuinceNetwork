namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class CommentLike
    {
        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        public virtual Comment Comment { get; set; }
    }
}