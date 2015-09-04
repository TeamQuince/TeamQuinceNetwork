namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Models.BindingModels;
    using Models.ViewModels;
    using SocialNetwork.Models;
    using Filters;
    using System.Collections.Generic;


    [Authorize]
    public class GroupsController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult CreateGroup(CreateGroupBindingModel group)
        {
            if (group == null)
            {
                return this.BadRequest("Group model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (this.Data.Groups.Any(g => g.Name == group.Name))
            {
                return this.BadRequest("The group name already exists");
            }

            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            this.Data.Groups.Add(new Group()
            {
                Name = group.Name,
                CreatedOn = DateTime.Now,
                Members = new List<ApplicationUser>()
                {
                    currentUser
                }
            });
            this.Data.SaveChanges();

            var viewModel = new CreateGroupViewModel()
            {
                Name = group.Name
            };

            return this.Ok(viewModel);
        }
    }
}