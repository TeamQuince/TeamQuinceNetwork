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

        public IHttpActionResult AddPost(int groupId, AddGroupPostBindingModel post)
        {
            var group = this.Data.Groups.SingleOrDefault(g => g.Id == groupId);
            if (group == null)
            {
                return this.BadRequest("Invalid group id");
            }

            if (post == null)
            {
                return this.BadRequest("Post model cannot be null");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var postOwnerId = this.User.Identity.GetUserId();
            var postOwner = this.Data.Users.FirstOrDefault(u => u.Id == postOwnerId);

            if (!group.Members.Any(m => m.Id == postOwnerId))
            {
                return this.BadRequest("You are not a member of the group");
            }

            var postObj = new GroupPost()
            {
                Content = post.postContent,
                PostedOn = DateTime.Now,
                Author = postOwner,
                Owner = group
            };

            group.Posts.Add(postObj);
            this.Data.SaveChanges();

            var viewModel = this.Data.GroupPosts
                .Where(p => p.Id == postObj.Id)
                .Select(GroupPostViewModel.Create);
            
            return this.Ok(viewModel);
        }

        [HttpPost]
        public IHttpActionResult Join(int groupId)
        {
            var group = this.Data.Groups.SingleOrDefault(g => g.Id == groupId);
            
            if (group == null)
            {
                return this.BadRequest("Invalid group id!");
            }

            var currentUserId = this.User.Identity.GetUserId();

            if (group.Members.Any(m => m.Id == currentUserId))
            {
                return this.BadRequest("You have already joined this group!");
            }

            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);

            group.Members.Add(currentUser);
            this.Data.SaveChanges();

            return this.Ok(string.Format("You have successfuly joined group {0}.", group.Name));
        }

        [HttpPut]
        public IHttpActionResult Leave(int groupId)
        {
            var group = this.Data.Groups.FirstOrDefault(g => g.Id == groupId);
            var currentUserId = this.User.Identity.GetUserId();

            if (group == null)
            {
                return this.BadRequest("Invalid group id!");
            }

            var member = group.Members.SingleOrDefault(m => m.Id == currentUserId);

            if (member == null)
            {
                return this.BadRequest("You are not a member of this group!");
            }

            group.Members.Remove(member);
            this.Data.SaveChanges();

            return this.Ok(string.Format("You have successfuly left group {0}.", group.Name));
        }
    }
}