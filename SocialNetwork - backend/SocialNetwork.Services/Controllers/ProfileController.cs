namespace SocialNetwork.Services.Controllers
{
    using System.Linq;
    using System.Web.OData;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;
    using System.Threading;
    using SocialNetwork.Models.Enumerations;

    [Authorize]
    [RoutePrefix("api/me")]
    public class ProfileController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetDataAboutMeAsync()
        {
            var me = GetCurrentUser();
            if (me == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            return await this.Ok(new ProfileViewModel(me)).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [EnableQuery]
        [Route("friends")]
        public async Task<HttpResponseMessage> GetOwnFriendsAsync()
        {
            var me = GetCurrentUser();
            if (me == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            var friends = me.Friends.AsQueryable().Select(FriendViewModel.Create).ToList();

            return await this.Ok(friends).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [Route("requests")]
        public async Task<HttpResponseMessage> GetFriendRequests()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            var requests = currentUser.Requests
                .Where(r => r.Status == FriendRequestStatus.Pending)
                .Select(r => new
                {
                    id = r.Id,
                    status = r.Status.ToString(),
                    user = new
                    {
                        id = r.Sender.Id,
                        userName = r.Sender.UserName,
                        name = r.Sender.Name,
                        image = r.Sender.ProfilePicture
                    }
                })
                .ToList();

            return await this.Ok(requests).ExecuteAsync(new CancellationToken());

        }

        [HttpPost]
        [Route("requests/{username}")]
        public async Task<HttpResponseMessage> SendFriendRequest([FromUri] string username)
        {
            var targetUser = this.Data.Users.FirstOrDefault(u => u.UserName == username);

            if (targetUser == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            if (currentUser == targetUser)
            {
                return await this.BadRequest("You cannot send friendship request to yourself.").ExecuteAsync(new CancellationToken());
            }

            if (targetUser.Friends.Contains(currentUser))
            {
                return await this.BadRequest("User is already a friend.").ExecuteAsync(new CancellationToken());
            }

            if (targetUser.Requests.Any(r => r.Sender == currentUser))
            {
                return await this.BadRequest("A request already exists.").ExecuteAsync(new CancellationToken());
            }

            var request = new FriendshipRequest()
            {
                Sender = currentUser,
                Recipient = targetUser,
                Status = FriendRequestStatus.Pending
            };

            targetUser.Requests.Add(request);
            this.Data.SaveChanges();

            return await this.Ok("Friend request sent.").ExecuteAsync(new CancellationToken());
        }

        [HttpPut]
        [Route("requests/{id}")]
        public async Task<HttpResponseMessage> ProcessFriendRequest([FromUri] int id, [FromUri] string status)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return await this.BadRequest("Invalid user token! Please login again!").ExecuteAsync(new CancellationToken());
            }

            var request = currentUser.Requests.FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return await this.NotFound().ExecuteAsync(new CancellationToken());
            }

            if (request.Status == FriendRequestStatus.Approved)
            {
                return await this.BadRequest("Request has already been approved.").ExecuteAsync(new CancellationToken());
            }
            else if (request.Status == FriendRequestStatus.Rejected)
            {
                return await this.BadRequest("Request has already been rejected.").ExecuteAsync(new CancellationToken());
            }

            switch (status)
            {
                case "approved":
                    request.Status = FriendRequestStatus.Approved;
                    currentUser.Friends.Add(request.Sender);
                    request.Sender.Friends.Add(currentUser);
                    this.Data.SaveChanges();
                    return await this.Ok("Friend request approved.").ExecuteAsync(new CancellationToken());
                case "delete":
                    request.Status = FriendRequestStatus.Rejected;
                    this.Data.SaveChanges();
                    return await this.Ok("Friend request approved.").ExecuteAsync(new CancellationToken());
                default:
                    return await this.BadRequest("Unkown status.").ExecuteAsync(new CancellationToken());
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

            var me = GetCurrentUser();
            if (me == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

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

        private ApplicationUser GetCurrentUser()
        {
            var myUserId = User.Identity.GetUserId();
            return this.Data.Users.Find(myUserId);
        }

    }
}
