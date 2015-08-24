namespace TQSN.Model
{
    public class Friendship
    {
        public int Id { get; set; }
    

        //AUTHOR OF FRIENDSHIP REQUEST
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        //RECIPIENT
        public string RecipientId { get; set; }
        public virtual ApplicationUser Recipient { get; set; }

        // STATUS OF FRIENDSHIP
        public FrendshipStatus FrendshipStatus { get; set; }

    }
}
