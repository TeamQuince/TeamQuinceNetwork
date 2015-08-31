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
        public BaseApiController()
            : this(new SocialNetworkContext())
        {

        }

        public BaseApiController(SocialNetworkContext context)
        {
            this.Data = context;
        }

        protected SocialNetworkContext Data { get; set; }
    }
}