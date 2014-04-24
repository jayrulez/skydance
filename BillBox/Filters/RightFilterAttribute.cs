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
                
                //if (!isAuthorized)
                //{
                //    return false;
                //}

                var user = Util.GetLoggedInUser();

                if (user == null)
                {
                    return false;
                }

                bool authorized = RightName == null || user.HasRight(this.RightName);

                if(!authorized)
                {
                    this.errorMessage = "You are not authorized to perform this action.";
                }

                return authorized;

            }
            catch(Exception ex)
            {
                throw ex;
                var isHandled = Util.HandleException(ex.GetBaseException());
                this.errorMessage = ex.GetBaseException().Message;
                this.isException = true;
                return false;
            }
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            //if (this.isException)
            //{
                filterContext.HttpContext.Response.Redirect(urlHelper.Action("Error", "Default", new { msg = this.errorMessage }));
            //}
            //else
            //{
            //    filterContext.HttpContext.Response.Redirect(urlHelper.Action("Login", "Default"));
            //}
        }
    }
}