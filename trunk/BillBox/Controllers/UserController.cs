using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using BillBox.Models;
using BillBox.Common;

using PagedList;
using System.Data.Entity.Infrastructure;

namespace BillBox.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        public ActionResult Create()
        {            
            LoadLookupValues(ViewBag);
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
                    user.PasswordExpireAt = DateTime.Now.AddDays(Util.GetPasswordExpirationDays());

                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = user.UserId });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }            
                
            LoadLookupValues(ViewBag);

            return View(user);
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Users);

            var users = dbContext.Users
                .Include(u => u.UserLevel)
                .Include(u => u.Agent)
                .Include(u => u.AgentBranch)
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);            

            return View(users);
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var user = dbContext.Users.Find(id);

            if (user == null)            
                return HttpNotFound();
            
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var user = dbContext.Users.Find(id);

            if (user == null)            
                return HttpNotFound();
            
            LoadLookupValues(ViewBag, user.AgentId ?? 0);

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
                    if (user.Password == null)
                    {
                        user.Password = String.Empty;
                    }              

                    dbContext.Users.Attach(user);

                    var entry = dbContext.Entry(user);
                    entry.State = System.Data.EntityState.Modified;


                    if (user.Password == String.Empty)
                    {
                        entry.Property(u => u.Password).IsModified = false;
                    }
                    else /*triggered if the password was updated/changed*/
                    {
                        user.PasswordExpireAt = DateTime.Now.AddDays(Util.GetPasswordExpirationDays());
                    }
                    
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = user.UserId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            LoadLookupValues(ViewBag, user.AgentId ?? 0);

            return View(user);

        }

        [HttpGet]
        public ActionResult AgentBranches(int? id)
        {
            ViewBag.AgentBranches = dbContext.AgentBranches.Where(ab => ab.AgentId == id);

            return PartialView("_AgentBranches");
        }

        private void LoadLookupValues(dynamic dictionary, int agentId = 0)
        {
            dictionary.Parishes = dbContext.Parishes.OrderBy(p => p.Name);
            dictionary.UserLevels = dbContext.UserLevels.OrderBy(ul => ul.LevelName);
            dictionary.Agents = dbContext.Agents.OrderBy(a => a.Name);

            if (agentId != 0)
                ViewBag.AgentBranches = dbContext.AgentBranches.Where(ab => ab.AgentId == agentId);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}
