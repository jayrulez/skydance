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
using BillBox.Filters;

namespace BillBox.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private Entities dbContext = new Entities();

        [RightFilter(RightName = "CREATE_USER")]
        public ActionResult Create()
        {
            try
            {
                LoadLookupValues(ViewBag);
                return View();
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_USER")]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.PasswordExpireAt = DateTime.Now.AddDays(Util.GetPasswordExpirationDays());

                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = user.UserId });

                }
                else
                {
                    LoadLookupValues(ViewBag);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

            return View(user);
        }

        [RightFilter(RightName = "VIEW_USERS")]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Users);

            try
            {
                var totalRecords = dbContext.Users.Count();
                var users = dbContext.Users
                    .Include(u => u.UserLevel)
                    .Include(u => u.Agent)
                    .Include(u => u.AgentBranch)
                    .OrderBy(a => a.Name)
                    .ToPagedList(pageNumber, pageSize);

                Util.PreparePagerInfo(ControllerContext.RequestContext, ViewBag, "Index", pageNumber, pageSize, totalRecords);

                return View(users);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [RightFilter(RightName = "VIEW_USER")]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var user = dbContext.Users.Find(id);

                if (user == null)
                    return HttpNotFound();

                return View(user);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_USER")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var user = dbContext.Users.Find(id);

                if (user == null)
                    return HttpNotFound();

                LoadLookupValues(ViewBag, user.AgentId ?? 0);

                return View(user);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
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
                else
                {
                    LoadLookupValues(ViewBag, user.AgentId ?? 0);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        public ActionResult AgentBranches(int? id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                ViewBag.AgentBranches = dbContext.AgentBranches.Where(ab => ab.AgentId == id);

                return PartialView("_AgentBranches");
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }            
        }

        private void LoadLookupValues(dynamic dictionary, int agentId = 0)
        {
            dictionary.Parishes = dbContext.Parishes.OrderBy(p => p.Name);
            dictionary.UserLevels = dbContext.UserLevels.OrderBy(ul => ul.LevelName);
            dictionary.Agents = dbContext.Agents.OrderBy(a => a.Name);

            if (agentId != 0)
                dictionary.AgentBranches = dbContext.AgentBranches.Where(ab => ab.AgentId == agentId);
        }

        private RedirectToRouteResult HandleErrorOnController(Exception exception)
        {
            string errorMessage;
            bool isHandled = Util.HandleException(exception, out errorMessage);

            if (isHandled)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("Error", "Default", null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}
