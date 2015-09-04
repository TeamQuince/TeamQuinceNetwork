namespace SocialNetwork.Services.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGroupViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}