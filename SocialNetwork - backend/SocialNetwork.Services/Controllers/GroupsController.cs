namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using SocialNetwork.Models;
    using SocialNetwork.Services.Models.BindingModels;
    using SocialNetwork.Services.Models.ViewModels;

    [Authorize]
    [RoutePrefix("api/groups")]
    public class GroupsController : BaseApiController
    {
        
    }
}
