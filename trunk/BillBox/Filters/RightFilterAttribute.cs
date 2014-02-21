using BillBox.Common;
using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillBox.Filters
{
    public class RightFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public string RightName;

        public RightFilterAttribute(string rightName = null)
        {
            this.RightName = rightName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = Util.GetLoggedInUser();

            if (RightName == null)
            {

            }else if (user == null || !user.HasRight(this.RightName))
            {
                throw new HttpException(403, "You are not authorized to view this page.");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}