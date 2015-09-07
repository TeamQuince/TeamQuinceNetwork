namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/groups")]
    public class GroupsController : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateGroup(CreateGroupBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Missing data about group.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (this.Data.Groups.Any(g => g.Name == model.Name))
            {
                return this.BadRequest("The group already exists.");
            }

            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var group = new Group()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                WallPicture = model.WallPicture,
                Owner = currentUser
            };

            this.Data.Groups.Add(group);
            this.Data.SaveChanges();

            var viewModel = CreateGroupViewModel.CreateGroupPreview(currentUser, group);

            return this.Ok(viewModel);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetGroupById([FromUri] int id)
        {
            var group = this.Data.Groups.Find(id);

            if (group == null)
            {
                return this.NotFound();
            }

            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            return this.Ok(CreateGroupViewModel.CreateGroupPreview(currentUser, group));
        }

        [HttpGet]
        [Route("{username}/list")]
        public IHttpActionResult GetGroupsForUser([FromUri] string username)
        {
            var user = this.Data.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return this.NotFound();
            }

            var userId = user.Id;

            var groupsWhereMember = user.Groups
                .Select(g => CreateGroupViewModel.CreateGroupPreview(user, g))
                .ToList();

            var groupsWhereOwner = this.Data.Groups
                .Where(g => g.Owner.Id == userId)
                .ToList()
                .Select(g => CreateGroupViewModel.CreateGroupPreview(user, g))
                .ToList();

            var totalGroups = groupsWhereMember.Union(groupsWhereOwner);

            return this.Ok(totalGroups);
        }

        [HttpPost]
        [Route("{groupId}/join")]
        public IHttpActionResult Join([FromUri] int groupId)
        {
            var group = this.Data.Groups.Find(groupId);

            if (group == null)
            {
                return this.NotFound();
            }

            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (group.Members.Any(m => m.Id == currentUserId))
            {
                return this.BadRequest("You are already a member of this group.");
            }

            group.Members.Add(currentUser);
            this.Data.SaveChanges();

            return this.Ok(CreateGroupViewModel.CreateGroupPreview(currentUser, group));
        }

        [HttpPost]
        [Route("{groupId}/leave")]
        public IHttpActionResult Leave(int groupId)
        {
            var group = this.Data.Groups.Find(groupId);

            var currentUserId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (group == null)
            {
                return this.NotFound();
            }

            var member = group.Members.FirstOrDefault(m => m.Id == currentUserId);

            if (member == null)
            {
                return this.BadRequest("You are not a member of this group.");
            }

            group.Members.Remove(member);
            this.Data.SaveChanges();

            return this.Ok(CreateGroupViewModel.CreateGroupPreview(currentUser, group));
        }

        [HttpPut]
        [Route("{groupId}")]
        public IHttpActionResult EditGroupById([FromUri] int groupId, [FromBody] EditGroupBindingModel model)
        {
            var group = this.Data.Groups.Find(groupId);
            if (group == null)
            {
                return this.NotFound();
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (currentUser != group.Owner)
            {
                return this.BadRequest("Not allowed. User must be owner of group.");
            }

            group.Name = model.Name;
            group.Description = model.Description;
            group.WallPicture = model.WallPicture;
            this.Data.SaveChanges();

            var dbGroup = this.Data.Groups.Find(group.Id);

            return this.Ok(CreateGroupViewModel.CreateGroupPreview(currentUser, dbGroup));
        }

        [HttpDelete]
        [Route("{groupId}")]
        public IHttpActionResult DeleteGroupById([FromUri] int groupId)
        {
            return this.BadRequest("The functionality is not supported at this time.");

            var group = this.Data.Groups.Find(groupId);

            if (group == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (currentUser != group.Owner)
            {
                return this.BadRequest("Not allowed. User must be owner of group.");
            }

            foreach (var post in group.Posts)
            {
                post.Comments.Clear();
            }
            group.Posts.Clear();
            group.Members.Clear();
            this.Data.Groups.Remove(group);
            this.Data.SaveChanges();
            return this.Ok();
        }

        [HttpGet]
        [Route("{groupId}/members")]
        public IHttpActionResult GetGroupMembersPreview([FromUri] int groupId)
        {
            var group = this.Data.Groups.Find(groupId);
            if (group == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var friendsPreview = new
            {
                Owner = new
                {
                    Name = group.Owner.Name,
                    UserName = group.Owner.UserName,
                    Id = group.Owner.Id,
                    ProfileImageData = group.Owner.ProfilePicture
                },
                Members = group.Members.Select(m => new
                {
                    Name = group.Owner.Name,
                    UserName = group.Owner.UserName,
                    Id = group.Owner.Id,
                    ProfileImageData = m.ProfilePicture
                })
            };

            return this.Ok(friendsPreview);
        }

        [HttpGet]
        [Route("{groupId}/wall")]
        public IHttpActionResult GetGroupWall([FromUri] int groupId)
        {
            var group = this.Data.Groups.Find(groupId);
            if (group == null)
            {
                return this.NotFound();
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var posts = group.Posts
                .OrderByDescending(p => p.PostedOn)
                .AsQueryable();

            var postsPreview = posts
                .Select(p => PostViewModel.CreateGroupPostPreview(currentUser, p))
                .ToList();

            return this.Ok(postsPreview);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchGroupByName([FromUri] string searchTerm)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            string searchName = searchTerm.ToUpper();

            var groupsFound = this.Data.Groups
                .Where(g => g.Name.ToUpper().Contains(searchName) || g.Description.ToUpper().Contains(searchName))
                .ToList()
                .Select(g => CreateGroupViewModel.CreateGroupPreview(currentUser, g));

            return this.Ok(groupsFound);
        }
    }
}