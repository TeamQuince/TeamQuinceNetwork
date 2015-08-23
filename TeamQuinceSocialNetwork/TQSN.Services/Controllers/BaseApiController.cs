namespace TQSN.Services.Controllers
{
    using System.Web.Http;
    using Data;

    public class BaseApiController : ApiController
    {
        public BaseApiController() : this(new ApplicationDbContext())
        {
        }

        public BaseApiController(ApplicationDbContext data)
        {
            this.Data = data;
        }

        protected ApplicationDbContext Data { get; set; }
    }
}

