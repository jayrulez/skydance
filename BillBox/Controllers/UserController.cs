using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BillBox.Models;
using BillBox.Models.Repository;
using BillBox.Common;

//using PagedList;

namespace BillBox.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //public ActionResult Index(int? page)
        //{
        //    /*
        //    IResponse<User> response = repository.GetAll();
            
        //    if(!response.IsSuccessful)
        //    {
        //        throw new HttpException(404, "Could not get users.");
        //    }else{
        //        ViewBag.users = response.Result.ToPagedList(); 
        //    }
        //    */

        //    Entities dbContext = new Entities();

        //    var users = dbContext.Users.OrderBy(u => u.Name);

        //    var pageNumber = page ?? 1;

        //    ViewBag.users = users.ToPagedList(pageNumber, 25);

        //    return View();
        //}

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(CreateUserModel model)
        //{
        //    return View(model);
        //}

        //public ActionResult Update()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Update(UpdateUserModel model)
        //{
        //    return View(model);
        //}

    }
}
