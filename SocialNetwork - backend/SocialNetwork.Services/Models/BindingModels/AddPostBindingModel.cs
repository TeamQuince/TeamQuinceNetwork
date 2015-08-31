namespace SocialNetwork.Services.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class AddPostBindingModel
    {
        [Required]
        [MinLength(2)]
        public string postContent { get; set; }

        [Required]
        public string username { get; set; }
    }
}