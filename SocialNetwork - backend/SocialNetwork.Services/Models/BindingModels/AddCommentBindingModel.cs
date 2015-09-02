namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class AddCommentBindingModel
    {
        [Required]
        [MinLength(2)]
        public string commentContent { get; set; }
    }
}