using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BillBox.Common;
using BillBox.Models;
using BillBox.Models.Repository;

namespace BillBox.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            UserRepository userRepository = new UserRepository();

            IResponse<User> response = userRepository.GetUser("admin");

            if(response.IsSuccessful)
            {
                return Content(response.Result.Username);
            }

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
