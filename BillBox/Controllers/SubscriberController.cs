using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillBox.Controllers
{
    [Authorize]
    public class SubscriberController : Controller
    {
        //
        // GET: /Subscriber/

        public ActionResult Index()
        {
            return View();
        }

    }
}
