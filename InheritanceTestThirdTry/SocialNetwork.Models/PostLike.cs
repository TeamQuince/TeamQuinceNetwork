﻿namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostLike
    {
        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        public virtual Post Post { get; set; }
    }
}