using BillBox.Common;
using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillBox.Filters
{
    public class RightFilterAttribute : AuthorizeAttribute
    {
        public string RightName {get;set;}

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);

            if(!isAuthorized)
            {
                return false;
            }

            User user = Util.GetLoggedInUser();

            if(user == null)
            {
                return false;
            }

            return RightName == null || user.HasRight(this.RightName);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            filterContext.Controller.TempData["ErrorMessage"] = "You are not authorized to view this page.";

            filterContext.HttpContext.Response.Redirect(urlHelper.Action("Error", "Default"));
        }
    }
}