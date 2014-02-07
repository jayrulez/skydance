using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BillBox.Models;
using System.Web.Security;

namespace BillBox.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
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
        public ActionResult Login(LoginModel model)
        {
            Entities db = new Entities();

            if(ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Username == model.Username && u.Password == model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.Autologin);

                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
