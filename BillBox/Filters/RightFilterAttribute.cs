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
        public string RightName;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = Util.GetLoggedInUser();

            if (RightName == null)
            {
                return true;
            }
            else if (user == null || !user.HasRight(this.RightName))
            {
                return false;
            }

            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User user = Util.GetLoggedInUser();

            if (RightName == null)
            {

            }
            else if (user == null || !user.HasRight(this.RightName))
            {
                throw new HttpException(403, "You are not authorized to view this page.");
            }

            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    User user = Util.GetLoggedInUser();

        //    if (RightName == null)
        //    {

        //    }else if (user == null || !user.HasRight(this.RightName))
        //    {
        //        throw new HttpException(403, "You are not authorized to view this page.");
        //    }

        //    base.OnActionExecuting(filterContext);
        //}
    }
}