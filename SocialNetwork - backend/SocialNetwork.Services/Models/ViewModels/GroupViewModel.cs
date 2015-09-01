namespace SocialNetwork.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SocialNetwork.Models;

    public class GroupViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUsername { get; set; }

        public string WallPicture { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MembersCount { get; set; }

        public static Expression<Func<Group, GroupViewModel>> Create
        {
            get
            {
                return group => new GroupViewModel()
                {
                    Id = group.Id,
                    OwnerId = group.Owner.Id,
                    OwnerUsername = group.Owner.UserName,
                    WallPicture = group.WallPicture,
                    Name = group.Name,
                    Description = group.Description,
                    MembersCount = group.Members.Count
                };
            }
        }
    }
}