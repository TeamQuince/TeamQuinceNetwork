
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
        public IHttpActionResult UserFullData(string username)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
            }
            var resultUser = FullUserDataViewModel.GetFullUserData(searchedUser, currentUser);
            return this.Ok(resultUser);
        }

        //Endpoint: GET =>/api/usersinfo/{username}/preview => returns short info about user.
        [HttpGet]
        [Route("{username}/preview")]
        public IHttpActionResult UserPreviewData(string username)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
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
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            string searchName = searchTerm.ToUpper();

            var users =
                Data.Users.Where(u => u.Name.ToUpper().Contains(searchName))
                    .ToList()
                    .Select(u => PreviewUserDataViewModel.GetPreviewUserData(u, currentUser));

            return this.Ok(users);
        }

        //Endpoint: GET =>/api/usersinfo/{username}/wall?StartPostId=&PageSize=5 => return User (byUsername) wall (colection of posts) by pages.

        [HttpGet]
        [Route("{username}/wall")]
        public IHttpActionResult GetWall(string username, [FromUri]WallBindingModel wall)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
            }
            if (currentUser.Friends.Contains(searchedUser) == false)
            {
                return this.BadRequest("you can not retrieve non friend data");
            }

            //TO DO check if current user and searched user are the same -> redirect to view my own wall

            IQueryable<Post> postCollection = searchedUser.Posts.AsQueryable().OrderByDescending(p => p.PostedOn);
            if (wall.StartPostId != null)
            {
                postCollection = postCollection.SkipWhile(p => p.Id != wall.StartPostId).Skip(1).AsQueryable();
            }

            var postPage = postCollection.Take(wall.PageSize).Select(post => new PostViewModel()
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


            return this.Ok(postPage);
        }


        //Endpoint: GET =>/api/usersinfo/{username}/friends => return FriendCollection of user (byUsername) preview data. Can be seen only friend's friends.
        [HttpGet]
        [Route("{username}/friends")]
        public IHttpActionResult FriendsData(string username)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
            }
            if (currentUser.Friends.Contains(searchedUser) == false)
            {
                return this.BadRequest("you can not retrieve non friend data");
            }

            var friends = FriendsCollectionViewModel.Create(searchedUser);
            return this.Ok(friends);
        }


        //Endpoint: GET =>/api/users/{username}/friends/preview => return short FriendCollection(6 priends) of  user (byUsername) preview data. Can be seen only friend's friends.
        [HttpGet]
        [Route("{username}/friends/preview")]
        public IHttpActionResult FriendsPreviewData(string username)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest("login to retrieve the data");
            }
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
            }
            if (currentUser.Friends.Contains(searchedUser) == false)
            {
                return this.BadRequest("you can not retrieve non friend data");
            }

            var friends = ShortFriendsCollectionViewModel.Create(searchedUser);
            return this.Ok(friends);
        }



    }
}
