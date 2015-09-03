namespace SocialNetwork.Services.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// Custum Filter for validating model for null and model state
    /// </summary>
    public class ValidateModel : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> validate;

        public ValidateModel() : this(args => args.ContainsValue(null))
        {
        }

        public ValidateModel(Func<Dictionary<string, object>, bool> condition)
        {
            this.validate = condition;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // CHECK NULL
            if (validate(actionContext.ActionArguments))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                   HttpStatusCode.BadRequest, "arguments cannot be null");
            }

            // VALIDATE MODEL STATE
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}