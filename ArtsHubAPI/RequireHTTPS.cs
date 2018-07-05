using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ArtsHubAPI
{
    public class RequireHTTPS : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string redirectUrl = null;
            //HttpContext context = HttpContext.Current;
            //if (!context.Request.IsSecureConnection)
            //{
            //    redirectUrl = context.Request.Url.ToString().Replace("http:", "https:");
            //    UriBuilder uri = new UriBuilder(redirectUrl);
            //    uri.Port = 44312;
            //    context.Response.Redirect(uri.ToString());
            //}

            //else
            //{
                base.OnAuthorization(actionContext);
            //}
        }
    }
}