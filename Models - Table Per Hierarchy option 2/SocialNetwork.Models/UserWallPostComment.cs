namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserWallPostComment : Posting
    {
            [Required]
            public int UserPostId { get; set; }
            public virtual Posting UserPost { get; set; }
     }
}
