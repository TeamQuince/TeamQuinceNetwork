namespace SocialNetwork.Services.Models.BindingModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentBindingModel
    {
        [Required]
        [MinLength(1)]
        public string CommentContent { get; set; }
    }
}