namespace TQSN.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FriendshipRequest
    {
        public int Id { get; set; }

        //AUTHOR OF FRIENDSHIP REQUEST
        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        [InverseProperty("FriendshipRequestsAuthor")]
        public virtual ApplicationUser Author { get; set; }

        //RECIPIENT
        [Required]
        [ForeignKey("Recipient")]
        public string RecipientId { get; set; }
        [InverseProperty("FriendshipRequestsRecipient")]
        public virtual ApplicationUser Recipient { get; set; }

        // STATUS OF FRIENDSHIP
        [Required]
        public FrendshipRequestStatus FrendshipRequestStatus { get; set; }

    }
}
