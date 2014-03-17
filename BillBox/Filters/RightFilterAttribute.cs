using BillBox.Common;
using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace BillBox.Filters
{
    public class RightFilterAttribute : AuthorizeAttribute
    {
        public string RightName {get;set;}

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {    
            try
            {
                bool isAuthorized = base.AuthorizeCore(httpContext);

                if (!isAuthorized)
                {
                    return false;
                }

                var user = Util.GetLoggedInUser();

                if (user == null)
                {
                    return false;
                }

                return RightName == null || user.HasRight(this.RightName);

            }
            catch (Exception ex)
            {
                return true;
                //string errorMessage;
                //var isHandled = Util.HandleException(ex.GetBaseException(), out errorMessage);

                //if (isHandled)
                //{
                    //put errorMessage in tempdata
                //}

                //httpContext.Response.Redirect("Default/Error");
            }
            
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