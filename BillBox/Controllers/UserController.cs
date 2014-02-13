using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using BillBox.Models;
using BillBox.Models.Repository;
using BillBox.Common;

using PagedList;

namespace BillBox.Controllers
{
    public class UserController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var users = dbContext.Users
                .Include(u => u.UserLevel)
                .Include(u => u.Agent)
                .Include(u => u.AgentBranch)
                .OrderBy(a => a.Name);
            
            var pageNumber = page ?? 1;
            
            ViewBag.users = users.ToPagedList(pageNumber, 25);

            return View();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Parishes = new SelectList(dbContext.Parishes, "ParishId", "Name");
            ViewBag.UserLevels = new SelectList(dbContext.UserLevels, "LevelId", "LevelName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int userId = 0)
        {
            User user = dbContext.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int userId = 0)
        {
            User user = dbContext.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(user).State = System.Data.EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { userId = user.UserId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(user);
            
        }

    }
}
