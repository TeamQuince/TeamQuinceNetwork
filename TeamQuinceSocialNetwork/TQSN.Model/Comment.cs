namespace TQSN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment: ICommentLikeReceiver
    {
        private ICollection<Comment> _comments;
        private ICollection<Like> _likes;

        public Comment()
        {
            this._comments = new List<Comment>();
            this._likes = new HashSet<Like>();
        }        

        // ID
        [Key]
        public int Id { get; set; }

        // CONTENT
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        // COMMENTED ON DATE
        [Required]
        public DateTime PostedOn { get; set; }

        // AUTHOR
        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        [InverseProperty("CommentAuthoredCollection")]
        public virtual ApplicationUser Author { get; set; }


        //Who is COMMENT Reciver
        public virtual ICommentLikeReceiver CommentReceiver
        {
            get
            {
                if (this.CommentReciverPostId == null)
                {
                    return this.CommentReciverComment;
                }
                return this.CommentReciverPost;
            }
        }
        public int? CommentReciverPostId { get; set; }
        public virtual Post CommentReciverPost { get; set; }
        public int? CommentReciverCommentId { get; set; }
        public virtual Comment CommentReciverComment { get; set; }


        //Recieved Comments and Likes
        public virtual ICollection<Comment> CommentsRecivedCollection
        {
            get { return this._comments; }
            set { this._comments = value; }
        }

        public virtual ICollection<Like> LikesReceivedCollection
        {
            get { return this._likes; }
            set { this._likes = value; }
        }
        public string Type {
            get { return "Comment"; } }

    }
}
