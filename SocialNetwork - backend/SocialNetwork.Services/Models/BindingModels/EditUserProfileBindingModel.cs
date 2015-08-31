namespace SocialNetwork.Services.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class EditUserProfileBindingModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string ProfileImageData { get; set; }

        public string CoverImageData { get; set; }

        public UserGender Gender { get; set; }
    }
}