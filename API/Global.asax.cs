using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace API
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            /*
            GlobalConfiguration.Configuration.Routes.Add("default", 
                new HttpRoute("{controller}"));

             
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
               name: "Api_Post",
               routeTemplate: "{controller}/{id}/{action}",
               defaults: new { id = RouteParameter.Optional, action = "Post" } 
            );
            */
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultApiWithId", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultApiGet", "api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            GlobalConfiguration.Configuration.Routes.MapHttpRoute("DefaultApiPost", "api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });



        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}