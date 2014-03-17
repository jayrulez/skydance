using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using BillBox.Common;
using BillBox.Models;


namespace BillBox.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        private Entities dbContext = new Entities();

        public ActionResult Index()
        {
            return RedirectToAction("NewBill", "Payment");
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
            try
            {
                if (ModelState.IsValid)
                {
                    var user = dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, model.Autologin);

                        var userRights = user.GetUserRights();

                        if (userRights.Count > 0)
                            Session["UserRights"] = userRights;

                        return RedirectToAction("Index", "Default");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }

                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

            return View(model);
        }
         
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Login", "Default");
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Test()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }

        private RedirectToRouteResult HandleErrorOnController(Exception exception)
        {
            string errorMessage;
            bool isHandled = Util.HandleException(exception, out errorMessage);

            if (isHandled)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("Error", "Default", null);
        }
    }
}
