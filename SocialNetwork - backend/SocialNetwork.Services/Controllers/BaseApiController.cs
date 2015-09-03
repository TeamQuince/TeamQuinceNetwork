namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using SocialNetwork.Data;

    public class BaseApiController : ApiController
    {
        protected const int ProfilePictureKilobytesLimit = 128;
        protected const int WallPictureKilobytesLimit = 1024;

        public BaseApiController()
            : this(new SocialNetworkContext())
        {

        }

        public BaseApiController(SocialNetworkContext context)
        {
            this.Data = context;
        }

        protected SocialNetworkContext Data { get; set; }

        protected bool ValidateProfilePictureSize(string imageDataURL)
        {
            // Image delete
            if (imageDataURL == null)
            {
                return true;
            }

            // Every 4 bytes from Base64 is equal to 3 bytes
            if ((imageDataURL.Length / 4) * 3 >= ProfilePictureKilobytesLimit * 1024)
            {
                return false;
            }

            return true;
        }

        protected bool ValidateWallPictureSize(string imageDataURL)
        {
            // Image delete
            if (imageDataURL == null)
            {
                return true;
            }

            // Every 4 bytes from Base64 is equal to 3 bytes
            if ((imageDataURL.Length / 4) * 3 >= WallPictureKilobytesLimit * 1024)
            {
                return false;
            }

            return true;
        }
    }
}