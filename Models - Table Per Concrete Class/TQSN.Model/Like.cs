namespace TQSN.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Like
    {
        // LIKE ID
        [Key]
        public int Id { get; set; }

        // AUTHOR ID
        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        [InverseProperty("LikesAuthoredCollection")]
        public virtual ApplicationUser Author { get; set; }

        //Who is LIKE Receiver
        public virtual ICommentLikeReceiver LikeReceiver
        {
            get
            {
                if (this.LikeReceiverPostId == null)
                {
                    return this.LikeReceiverComment;
                }
                return this.LikeReceiverPost;
            }
        }
        public int? LikeReceiverPostId { get; set; }
        public virtual Post LikeReceiverPost { get; set; }
        public int? LikeReceiverCommentId { get; set; }
        public virtual Comment LikeReceiverComment { get; set; }
    }
}
