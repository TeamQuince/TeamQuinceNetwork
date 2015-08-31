
namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/usersinfo")]
    public class UsersInfoController : BaseApiController
    {
        //Endpoint: GET =>/api/usersinfo/{name} => returns full info about user.
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult UserFullData(string name)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = Data.Users.FirstOrDefault(u => u.Id == currentUserId);
            var searchedUser = Data.Users.FirstOrDefault(u => u.Name.Equals(name));
            if (searchedUser == null)
            {
                return this.BadRequest("no such user");
            }
            var resultUser = new FullUserDataViewModel(searchedUser, currentUser);
            return this.Ok(resultUser);
        }

    }
}
