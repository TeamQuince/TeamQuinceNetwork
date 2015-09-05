namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;

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

        [HttpGet]
        public IHttpActionResult FindGroupById(int id)
        {
            var group = this.Data.Groups.SingleOrDefault(g => g.Id == id);

            if (group == null)
            {
                return this.BadRequest("Invalid group id!");
            }

            return this.Ok(group);
        }
    }
}