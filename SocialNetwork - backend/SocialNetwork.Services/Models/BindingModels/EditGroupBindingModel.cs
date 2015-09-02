namespace SocialNetwork.Services.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class EditGroupBindingModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string WallPicture { get; set; }
    }
}