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
        public ActionResult Create()
        {
            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>().OrderBy(p => p.Name);
            ViewBag.UserLevels = dbContext.UserLevels.AsEnumerable<BillBox.Models.UserLevel>();
            ViewBag.Agents = dbContext.Agents.AsEnumerable<BillBox.Models.Agent>().OrderBy(a => a.Name);

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
                    /*to be removed after db review*/
                    user.Designation = " ";
                    user.LoginStatus = 1;
                    user.PasswordExpireAt = DateTime.Now;

                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = user.UserId });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>().OrderBy(p => p.Name);
                ViewBag.UserLevels = dbContext.UserLevels.AsEnumerable<BillBox.Models.UserLevel>();
                ViewBag.Agents = dbContext.Agents.AsEnumerable<BillBox.Models.Agent>().OrderBy(a => a.Name);
            }

            return View(user);
        }

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
        public ActionResult Details(int id = 0)
        {
            User user = dbContext.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
         
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            User user = dbContext.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>();
            ViewBag.UserLevels = dbContext.UserLevels.AsEnumerable<BillBox.Models.UserLevel>();
            ViewBag.Agents = dbContext.Agents.AsEnumerable<BillBox.Models.Agent>();
            ViewBag.AgentBranches = dbContext.AgentBranches.AsEnumerable<BillBox.Models.AgentBranch>().Where(ab => ab.AgentId == user.AgentId);

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
                    /*to be removed after db review*/
                    user.Designation = " ";
                    user.LoginStatus = 1;
                    user.PasswordExpireAt = DateTime.Now;

                    var entry = dbContext.Entry(user);
                    entry.State = System.Data.EntityState.Modified;

                    if (user.Password == null)
                        entry.Property(u => u.Password).IsModified = false;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = user.UserId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>();
            ViewBag.UserLevels = dbContext.UserLevels.AsEnumerable<BillBox.Models.UserLevel>();
            ViewBag.Agents = dbContext.Agents.AsEnumerable<BillBox.Models.Agent>();
            ViewBag.AgentBranches = dbContext.AgentBranches.AsEnumerable<BillBox.Models.AgentBranch>().Where(ab => ab.AgentId == user.AgentId);

            return View(user);

        }

        [HttpGet]
        public ActionResult AgentBranches(int? id)
        {
            ViewBag.AgentBranches = dbContext.AgentBranches
                .Where(ab => ab.AgentId == id)
                .AsEnumerable<BillBox.Models.AgentBranch>();

            return PartialView("Branches");
        }

    }
}
