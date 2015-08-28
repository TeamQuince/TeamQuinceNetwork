namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Like
    {
        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }
    }
}
