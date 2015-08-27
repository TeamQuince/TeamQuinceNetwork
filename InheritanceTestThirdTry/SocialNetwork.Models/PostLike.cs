namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostLike
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Post Post { get; set; }
    }
}
