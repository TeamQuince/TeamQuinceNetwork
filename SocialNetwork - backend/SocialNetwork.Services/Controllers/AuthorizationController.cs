namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using System.Threading;
    using System.Text;

    [Authorize]
    [RoutePrefix("api/users")]
    public class AuthorizationController : BaseApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AuthorizationController()
        {
        }

        public AuthorizationController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // POST api/users/login
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> LoginUser(LoginUserBindingModel model)
        {
            // Invoke the "token" OWIN service to perform the login: /api/token
            // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/Token";
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.Username),
                    new KeyValuePair<string, string>("password", model.Password)
                };

                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;

                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };

                return responseMsg;
            }
        }

        // POST api/users/register
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> RegisterUser(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var user = new ApplicationUser() 
            { 
                UserName = model.Username,
                Name = model.Name,
                Gender = model.Gender,
                Email = model.Email
            };

            IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }

            // Auto login after register (successful user registration should return access_token)
            var loginResult = this.LoginUser(new LoginUserBindingModel()
            {
                Username = model.Username,
                Password = model.Password
            });
            return await loginResult;
        }

        // POST api/users/logout
        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok(new { message = "Logout successful." });
        }

        // PUT api/users/changepassword
        [HttpPut]
        [Route("changepassword")]
        public async Task<IHttpActionResult> ChangeUserPassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result = await this.UserManager.ChangePasswordAsync(
                User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok(
                new
                {
                    message = "Password changed successfully.",
                }
            );
        }

        // PUT api/users/profile
        [HttpPut]
        [Route("Profile")]
        public IHttpActionResult EditUserProfile(EditUserProfileBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Validate the current user exists in the database
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var isEmailTaken = this.Data.Users.Any(x => x.Email == model.Email);
            if (isEmailTaken)
            {
                return this.BadRequest("Invalid email. The email is already taken!");
            }

            currentUser.Name = model.Name;
            currentUser.Email = model.Email;
            currentUser.ProfilePicture = model.ProfileImageData;
            currentUser.WallPicture = model.CoverImageData;
            currentUser.Gender = model.Gender;

            this.Data.SaveChanges();

            return this.Ok(
                new
                {
                    message = "User profile edited successfully.",
                });
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}