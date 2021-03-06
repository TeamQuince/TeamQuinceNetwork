﻿namespace SocialNetwork.Services.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SocialNetwork.Models;
    using SocialNetwork.Models.Enumerations;

    public class RegisterBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public UserGender Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}