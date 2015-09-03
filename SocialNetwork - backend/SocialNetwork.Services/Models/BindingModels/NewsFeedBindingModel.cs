using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Services.Models.BindingModels
{
    public class NewsFeedBindingModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Value for {0} must be equal or greater than 1.")]
        public int? StartPostId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value for {0} must be equal or greater than 1.")]
        public int PageSize { get; set; }
    }
}