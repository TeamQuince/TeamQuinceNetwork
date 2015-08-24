namespace TQSN.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        // LIKE ID
        [Key]
        public int Id { get; set; }

        // POST ID
        [Required]
        public int? PostId { get; set; }

        // POST
        public virtual Post Post { get; set; }

        // COMMENT ID
        public int? CommentId { get; set; }

        // COMMENT
        public virtual Comment Comment { get; set; }

        // AUTHOR ID
        [Required]
        public string AuthorId { get; set; }

        // AUTHOR
        public virtual ApplicationUser Author { get; set; }
    }
}
