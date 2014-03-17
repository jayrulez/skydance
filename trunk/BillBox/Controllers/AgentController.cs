﻿using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using System.Data;
using BillBox.Common;
using BillBox.Filters;

namespace BillBox.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        [RightFilter(RightName = "VIEW_AGENTS")]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Agents);

            try
            {
                var agents = dbContext.Agents.OrderBy(a => a.Name).ToPagedList(pageNumber, pageSize);

                return View(agents);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpGet]
        [RightFilter(RightName = "CREATE_AGENT")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_AGENT")]
        public ActionResult Create(Agent agent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Agents.Add(agent);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { agentId = agent.AgentId });
                }
                else
                {
                    ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

            return View(agent);
        }



        [RightFilter(RightName = "VIEW_AGENT")]
        public ActionResult Details(int agentId = 0)
        {
            try
            {
                var agent = dbContext.Agents.Find(agentId);

                if (agent == null)
                {
                    return HttpNotFound();
                }

                return View(agent);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_AGENT")]
        public ActionResult Edit(int agentId = 0)
        {
            try
            {
                var agent = dbContext.Agents.Find(agentId);

                if (agent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

                return View(agent);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_AGENT")]
        public ActionResult Edit(Agent agent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Entry(agent).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { agentId = agent.AgentId });
                }
                else
                {
                    ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

            return View(agent);
        }

        [RightFilter(RightName = "CREATE_AGENT_BRANCH")]
        public ActionResult AddBranch(int agentId = 0)
        {
            try
            {
                var agent = dbContext.Agents.Find(agentId);

                if (agent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

                var branch = new AgentBranch()
                {
                    AgentId = agentId
                };

                return View(branch);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_AGENT_BRANCH")]
        public ActionResult AddBranch(AgentBranch model)
        {
            

            
                try
                {
                    var agent = dbContext.Agents.Find(model.AgentId);

                    if (agent == null)
                    {
                        return HttpNotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        dbContext.AgentBranches.Add(model);

                        dbContext.SaveChanges();

                        return RedirectToAction("ViewBranch", new { branchId = model.BranchId });
                    }
                    else
                    {
                        ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);
                    }
                }
                catch (Exception ex)
                {
                    return HandleErrorOnController(ex.GetBaseException());
                }

            return View(model);
        }

        [RightFilter(RightName = "EDIT_AGENT_BRANCH")]
        public ActionResult EditBranch(int branchId = 0)
        {
            try
            {
                var branch = dbContext.AgentBranches.Find(branchId);

                if (branch == null)
                {
                    return HttpNotFound();
                }

                ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

                return View(branch);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_AGENT_BRANCH")]
        public ActionResult EditBranch(AgentBranch model)
        {
           
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbContext.Entry(model).State = EntityState.Modified;

                        dbContext.SaveChanges();

                        return RedirectToAction("ViewBranch", new { branchId = model.BranchId });
                    }
                    else
                    {
                        ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);
                    }
                }
                catch (Exception ex)
                {
                    return HandleErrorOnController(ex.GetBaseException());
                }

            return View(model);
        }

        [RightFilter(RightName = "VIEW_AGENT_BRANCH")]
        public ActionResult ViewBranch(int branchId = 0)
        {
            var branch = dbContext.AgentBranches.Find(branchId);

            if (branch == null)
            {
                return HttpNotFound();
            }

            ViewBag.branch = branch;

            return View(branch);
        }

        [RightFilter(RightName = "VIEW_AGENT_BRANCHES")]
        public ActionResult ListBranches(int? page, int agentId = 0)
        {
            try
            {
                var agent = dbContext.Agents.Find(agentId);
                var pageNumber = page ?? 1;
                var pageSize = Util.GetPageSize(Common.PagedList.Branches);

                if (agent == null)
                {
                    return HttpNotFound();
                }

                var branches = dbContext.AgentBranches
                    .Where(b => b.AgentId == agentId)
                    .OrderBy(b => b.Name)
                    .ToPagedList(pageNumber, pageSize);

                return View(branches);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
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
