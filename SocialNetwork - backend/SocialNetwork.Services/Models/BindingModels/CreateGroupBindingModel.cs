namespace SocialNetwork.Services.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class CreateGroupBindingModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}