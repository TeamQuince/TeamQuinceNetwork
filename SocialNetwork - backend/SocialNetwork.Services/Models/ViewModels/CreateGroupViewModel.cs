namespace SocialNetwork.Services.Models.ViewModels
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using SocialNetwork.Models;

    public class CreateGroupViewModel
    {
        public static object CreateGroupPreview(ApplicationUser user, Group group) 
        {
            return new
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                WallPicture = group.WallPicture,
                Owner = new
                {
                    Name = group.Owner.Name,
                    Username = group.Owner.UserName,
                    Id = group.Owner.Id
                },
                Members = group.Members.Select(m => new
                {
                    Name = m.Name,
                    Username = m.UserName,
                    Id = m.Id
                }),
                IsMember = group.Members.Any(m => m == user),
                IsOwner = group.Owner == user
            };
        }
    }
}