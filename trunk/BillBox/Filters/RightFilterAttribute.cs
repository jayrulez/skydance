using BillBox.Common;
using BillBox.Models;
//using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Security.Policy;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace BillBox.Filters
{
    public class RightFilterAttribute : AuthorizeAttribute
    {
        public string RightName {get;set;}

        private bool isException = false;
        private string errorMessage = string.Empty;

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
            catch(Exception ex)
            {
                var isHandled = Util.HandleException(ex.GetBaseException(), out this.errorMessage);
                this.isException = true;
                return false;
            }
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            filterContext.HttpContext.Response.Redirect(urlHelper.Action("Error", "Default", new { msg = errorMessage }));

            //if(this.isException)
            //{
            //    filterContext.HttpContext.Response.Redirect(urlHelper.Action("Error", "Default", new { msg = errorMessage }));
            //}
            //else
            //{
            //    filterContext.HttpContext.Response.Redirect(urlHelper.Action("Login", "Default"));
            //}
        }
    }
}