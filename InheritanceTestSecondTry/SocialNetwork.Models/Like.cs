namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        // LIKE ID
        [Key]
        public int Id { get; set; }

        // POST ID
        [Required]
        public int PostingId { get; set; }

        // POST/COMMENT
        public virtual Posting Posting { get; set; }

        // AUTHOR ID
        [Required]
        public string AuthorId { get; set; }
        // AUTHOR
        public virtual User Author { get; set; }
    }
}
