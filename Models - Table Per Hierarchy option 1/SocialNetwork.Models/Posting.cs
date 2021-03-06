﻿namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Posting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public virtual User Author { get; set; }
    }
}
