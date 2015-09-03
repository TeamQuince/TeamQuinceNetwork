namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Models.BindingModels;
    using Models.ViewModels;

    using SocialNetwork.Models;

    [Authorize]
    [RoutePrefix("api/usersinfo")]
    public class UsersInfoController : BaseApiController
    {
        //Endpoint: GET =>/api/usersinfo/{username} => returns full info about user.
        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult UserFullData([FromUri] string username)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }
            
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.NotFound();
            }

            var userPreview = FullUserDataViewModel.GetFullUserData(searchedUser, currentUser);
            return this.Ok(userPreview);
        }

        //Endpoint: GET =>/api/usersinfo/{username}/preview => returns short info about user.
        [HttpGet]
        [Route("{username}/preview")]
        public IHttpActionResult UserPreviewData(string username)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.NotFound();
            }
            var resultUser = PreviewUserDataViewModel.GetPreviewUserData(searchedUser, currentUser);
            return this.Ok(resultUser);
        }

        //Endpoint: GET =>/api/usersinfo/search?searchTerm ={name} => return collection of users (in preview data) matching searchedName.
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchUserByName([FromUri] string searchTerm)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            string searchName = searchTerm.ToUpper();

            var users = this.Data.Users
                .Where(u => u.Name.ToUpper().Contains(searchName) || u.UserName.ToUpper().Contains(searchName))
                .ToList()
                .Select(u => PreviewUserDataViewModel.GetPreviewUserData(u, currentUser));

            return this.Ok(users);
        }

        //Endpoint: GET =>/api/usersinfo/{username}/wall?StartPostId=&PageSize=5 => return User (byUsername) wall (colection of posts) by pages.

        [HttpGet]
        [Route("{username}/wall")]
        public IHttpActionResult GetWall([FromUri] string username, [FromUri] WallBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Missing pagination parameters.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.NotFound();
            }

            //if (!currentUser.Friends.Contains(searchedUser))
            //{
            //    return this.BadRequest("you can not retrieve non friend data");
            //}

            var posts = searchedUser.Posts
                .OrderByDescending(p => p.PostedOn)
                .AsQueryable();

            if (model.StartPostId.HasValue)
            {
                posts = posts
                    .SkipWhile(p => p.Id != (int)model.StartPostId)
                    .Skip(1)
                    .AsQueryable();
            }

            var postsPreview = posts
                .Take(model.PageSize)
                .Select(post => new PostViewModel()
                {
                    Id = post.Id,
                    AuthorId = post.Author.Id,
                    AuthorUsername = post.Author.UserName,
                    AuthorProfileImage = post.Author.ProfilePicture,
                    WallOwnerId = post.Owner.Id,
                    PostContent = post.Content,
                    Date = post.PostedOn,
                    LikesCount = post.Likes.Count,
                    TotalCommentsCount = post.Comments.Count,
                    Comments = post.Comments
                        .OrderByDescending(c => c.PostedOn)
                        .AsQueryable()
                        .Select(CommentViewModel.Create).ToList()
                });

            return this.Ok(postsPreview);
        }


        //Endpoint: GET =>/api/usersinfo/{username}/friends => return FriendCollection of user (byUsername) preview data. Can be seen only friend's friends.
        [HttpGet]
        [Route("{username}/friends")]
        public IHttpActionResult GetFriendsFullData([FromUri] string username)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var searchedUser = this.Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.NotFound();
            }

            if (!currentUser.Friends.Contains(searchedUser) && currentUser != searchedUser)
            {
                return this.BadRequest("Not alllowed. You must be friends.");
            }

            var friends = FriendsFullDataViewModel.Create(searchedUser);
            return this.Ok(friends);
        }


        //Endpoint: GET =>/api/users/{username}/friends/preview => return short FriendCollection(6 priends) of  user (byUsername) preview data. Can be seen only friend's friends.
        [HttpGet]
        [Route("{username}/friends/preview")]
        public IHttpActionResult GetFriendsPreviewData([FromUri] string username)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.NotFound();
            }

            if (!currentUser.Friends.Contains(searchedUser) && currentUser != searchedUser)
            {
                return this.BadRequest("Not alllowed. You must be friends.");
            }

            var friends = FriendsPreviewDataViewModel.Create(searchedUser);
            return this.Ok(friends);
        }
    }
}