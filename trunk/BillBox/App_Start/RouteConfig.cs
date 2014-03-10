using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BillBox
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", 
                //Tests
                //defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Test", action = "GetUser", id = UrlParameter.Optional }
                //defaults: new { controller = "Test", action = "GetUsers", id = UrlParameter.Optional }
                //defaults: new { controller = "Test", action = "AddUsers", id = UrlParameter.Optional }
                //defaults: new { controller = "Report", action = "Collections", id = UrlParameter.Optional }
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}