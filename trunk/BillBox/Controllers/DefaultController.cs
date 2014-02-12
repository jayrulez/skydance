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
            IUserRepository repository = new UserRepository();

            if(ModelState.IsValid)
            {
                IResponse<User> response = repository.GetUser(model.Username);

                if(response.IsSuccessful)
                {
                    if (response.Result.LoginStatus == 1 && response.Result.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, model.Autologin);

                        return RedirectToAction("Index", "Default");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", response.Error.ToString());
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
