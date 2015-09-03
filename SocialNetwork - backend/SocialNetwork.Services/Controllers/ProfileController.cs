namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.OData;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;
    using System.Threading;
    using SocialNetwork.Models.Enumerations;
    using System.Data.Entity.Infrastructure;
    using SocialNetwork.Data;

    [Authorize]
    [RoutePrefix("api/me")]
    public class ProfileController : BaseApiController
    {
        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> GetDataAboutMeAsync()
        {
            var me = GetMeh();

            return await this.Ok(new ProfileViewModel(me)).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [EnableQuery]
        [Route("friends")]
        public async Task<HttpResponseMessage> GetOwnFriendsAsync()
        {
            var me = GetMeh();
            var friends = me.Friends.AsQueryable().Select(FriendViewModel.Create).ToList();

            return await this.Ok(friends).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [EnableQuery]
        [Route("requests")]
        public IHttpActionResult GetFrindReqeusts()
        {
            var me = GetMeh();

            var requests = me.Requests.AsQueryable().Select(FriendshipRequestViewModel.Create).ToList();

            return this.Ok(requests);
        }

        [HttpPost]
        [EnableQuery]
        [Route("requests/{username}")]
        public IHttpActionResult SendFriendRequest(string username)
        {
            var me = GetMeh();
            var otherUser = this.Data.Users.FirstOrDefault(u => u.UserName == username);
            if (otherUser == null)
            {
                return this.BadRequest("Records of User " + username + " do not exist in the database.");
            }

            var requestFromMe = otherUser.Requests.AsQueryable().FirstOrDefault(r => r.Recipient.Id == otherUser.Id);
            if (requestFromMe != null)
            {
                return this.BadRequest("You have already sent a frind request. Request's status is " + requestFromMe.Status.ToString());
            }
            else
            {
                var requestToMe = me.Requests.AsQueryable().FirstOrDefault(r => r.Sender.Id == me.Id);
                if (requestToMe != null)
                {
                    return this.BadRequest("A " + requestToMe.Status.ToString().ToLower() + " request already exists.");
                }
            }

            var request = new FriendshipRequest()
            {
                Status = FriendRequestStatus.Pending,
                Sender = me,
                Recipient = otherUser
            };

            this.Data.FriendshipRequests.Add(request);

            me.Requests.Add(request);
            this.Data.SaveChanges();
            otherUser.Requests.Add(request);
            this.Data.SaveChanges();

            return this.Ok(new FriendshipRequestViewModel(request));
        }

        [HttpPut]
        [EnableQuery]
        [Route("requests/{id}")]
        public IHttpActionResult ApproveFrindRequest(int id, [FromUri]string status)
        {
            var me = GetMeh();

            var request = me.Requests.AsQueryable().FirstOrDefault(r => r.Id == id);
            if (request == null)
            {
                return this.BadRequest("Friendship request with Id = " + id + " does not exist in Database records and/or in Frindehip requests for the current user.");
            }

            switch (status.ToLower())
            {
                case "approved":
                    {
                        var message = ProcessCommand(true, request, me);
                        return this.Ok(message);
                    }
                case "delete":
                    {
                        var message = ProcessCommand(false, request, me);
                        return this.Ok(message);
                    }
                default: return this.BadRequest("Invalid action against Friendship request.");
            }
        }

        [HttpGet]
        [EnableQuery]
        [Route("feed")]
        public IHttpActionResult GetNewsFeed([FromUri]NewsFeedBindingModel feedModel)
        {
            if (feedModel == null)
            {
                return this.BadRequest("No Page Size specified.");
            }
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var me = GetMeh();

            var postsFromToFriends = this.Data.Posts.AsQueryable()
                    .Where(p => p.Author.Friends.Any(fr => fr.Id == me.Id) ||
                    p.Owner.Friends.Any(fr => fr.Id == me.Id))
                    .Select(PostViewModel.Create)
                    .OrderByDescending(p => p.Date)
                    .ToList();

            if (feedModel.StartPostId.HasValue)
            {
                postsFromToFriends = postsFromToFriends
                    .SkipWhile(p => p.Id != feedModel.StartPostId).Skip(1).ToList();
            }

            var pagePosts = postsFromToFriends.Take(feedModel.PageSize).ToList();

            return this.Ok(pagePosts);
        }

        private MessageViewModel ProcessCommand(bool isApproved, FriendshipRequest request, ApplicationUser me)
        {
            MessageViewModel message = null;

            if (request.Status == FriendRequestStatus.Pending)
            {
                if (isApproved == false)
                {
                    message = DenyFriendRequest(request, me);
                }
                else
                {
                    message = ApproveFriendRequest(request, me);
                }
            }
            else if (request.Status == FriendRequestStatus.Approved)
            {
                message = new MessageViewModel("Friend request has already been approved. "
                    + request.Sender.UserName + " is among your friend list.");
            }
            else
            {
                message = new MessageViewModel("Friend request has already been denied.");
            }

            return message;
        }

        private MessageViewModel DenyFriendRequest(FriendshipRequest request, ApplicationUser me)
        {
            request.Status = FriendRequestStatus.Rejected;

            this.Data.SaveChanges();

            return new MessageViewModel("Friend request successfully declined.");
        }

        private MessageViewModel ApproveFriendRequest(FriendshipRequest request, ApplicationUser me)
        {
            request.Status = (FriendRequestStatus)1;
            me.Friends.Add(request.Sender);

            this.Data.SaveChanges();

            return new MessageViewModel("Friend request successfully approved.");
        }

        private ApplicationUser GetMeh()
        {
            var myUserId = User.Identity.GetUserId();
            return this.Data.Users.Find(myUserId);
        }

    }
}
