using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Billbox.Models.Interfaces;

namespace Billbox.Controllers
{
    public class TestController : Controller
    {
        private readonly IUserRepository UserRepository;

        public TestController(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        //
        // GET: /Test/

        public ActionResult Index()
        {
            var response = UserRepository.GetUser("RasMicah");
            if (response.IsSuccessful)
                ViewBag.Name = response.Result.Name;
            else
                ViewBag.Error = response.Error;
            return View();
        }

        //public ActionResult GetUser(

    }
}
