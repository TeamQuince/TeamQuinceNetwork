namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class WallBindingModel
    {
        public int? StartPostId { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}