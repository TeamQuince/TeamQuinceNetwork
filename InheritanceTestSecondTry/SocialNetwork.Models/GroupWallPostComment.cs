namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GroupWallPostComment : Posting
    {

        [Required]
        public int GroupPostId { get; set; }
        public virtual GroupWallPost GroupPost { get; set; }
    }
}