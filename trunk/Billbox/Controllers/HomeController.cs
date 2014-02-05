using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Billbox.Models;
using Billbox.Filters;
using WebMatrix.WebData;

namespace Billbox.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AgentUserLoginModel model)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.Autologin))
            {
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

    }
}
