namespace TQSN.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post: ICommentLikeReceiver
    {
        private ICollection<Comment> _comments;
        private ICollection<Like> _likes;

        public Post()
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

        // POSTED ON DATE
        
        [Required]
        public DateTime PostedOn { get; set; }

        //Posted on Wall
        public virtual WallPosts Wall {
            get {
                if (this.WallOwnerGroupId == null)
                {
                    return this.WallOwnerUser;
                }
                return this.WallOwnerGroup;
            }
        }
        [ForeignKey("WallOwnerUser")]
        public string WallOwnerUserId { get; set; }
     //   [InverseProperty("Wall")]
        public virtual  ApplicationUser WallOwnerUser { get; set; }
        [ForeignKey("WallOwnerGroup")]
        public int? WallOwnerGroupId { get; set; }
     //   [InverseProperty("Wall")]
        public virtual Group WallOwnerGroup { get; set; }


        // AUTHORED BY
        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        [InverseProperty("PostsAuthored")]
        public virtual ApplicationUser Author { get; set; }

      
        
        // COMMENTS AND LIKES RECIVED
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
            get { return "Post"; }
        }
    }
    
}
