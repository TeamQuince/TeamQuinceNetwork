namespace SocialNetwork.Services.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class GetNewsFeedBindingModel
    {
        public GetNewsFeedBindingModel()
        {
            this.StartPostId = 1;
            this.PageSize = 5;
        }

        [Range(0, 100000, ErrorMessage = "StartPostId should be in range [0...100000].")]
        public int? StartPostId { get; set; }

        [Range(1, 1000, ErrorMessage = "Page size be in range [1...1000].")]
        public int? PageSize { get; set; }
    }
}