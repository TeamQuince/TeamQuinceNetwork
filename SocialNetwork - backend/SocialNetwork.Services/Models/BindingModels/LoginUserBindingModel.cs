namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class LoginUserBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}