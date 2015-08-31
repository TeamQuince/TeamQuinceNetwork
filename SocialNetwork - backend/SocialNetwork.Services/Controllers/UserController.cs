
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
    [RoutePrefix("api/users")]
    public class UserController : BaseApiController
    {

        //Endpoint: GET =>/api/users/{name} => returns full info abt user.
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
