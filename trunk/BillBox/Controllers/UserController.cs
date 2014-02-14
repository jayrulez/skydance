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
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    RedirectToAction("Details", new { id = user.UserId });
                   
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>().OrderBy(p => p.Name);
            ViewBag.UserLevels = dbContext.UserLevels.AsEnumerable<BillBox.Models.UserLevel>();
            ViewBag.Agents = dbContext.Agents.AsEnumerable<BillBox.Models.Agent>().OrderBy(a => a.Name);

            return View(user);
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

        [HttpGet]
        public ActionResult AgentBranches(int? agentId)
        {
            ViewBag.AgentBranches = dbContext.AgentBranches
                .Where(ab => ab.AgentId == agentId)
                .AsEnumerable<BillBox.Models.AgentBranch>();

            return PartialView("Branches");
        }

    }
}
