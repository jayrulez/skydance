using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using System.Data;
using BillBox.Common;

namespace BillBox.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private Entities dbContext = new Entities();
        //
        // GET: /Agent/

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Agents);

            var agents = dbContext.Agents
                .OrderBy(a => a.Name)
                .ToPagedList(pageNumber, pageSize);

            return View(agents);
        }

        public ActionResult Create()
        {
            ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agent agent)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Agents.Add(agent);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { agentId = agent.AgentId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

            return View(agent);
        }

        public ActionResult Details(int agentId = 0)
        {
            Agent agent = dbContext.Agents.Find(agentId);

            if(agent == null)
            {
                return HttpNotFound();
            }

            return View(agent);
        }

        public ActionResult Edit(int agentId = 0)
        {
            Agent agent = dbContext.Agents.Find(agentId);

            if(agent == null)
            {
                return HttpNotFound();
            }

            ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

            return View(agent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent agent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(agent).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { agentId = agent.AgentId });
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.parishes = dbContext.Parishes.OrderBy(p => p.Name);

            return View(agent);
        }

        public ActionResult AddBranch(int agentId = 0)
        {
            var agent = dbContext.Agents.Find(agentId);

            if(agent == null)
            {
                return HttpNotFound();
            }

            var parishes = dbContext.Parishes.OrderBy(p => p.Name);

            ViewBag.parishes = parishes;

            AgentBranch branch = new AgentBranch();

            branch.AgentId = agentId;

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(AgentBranch model)
        {
            var agent = dbContext.Agents.Find(model.AgentId);

            if (agent == null)
            {
                return HttpNotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.AgentBranches.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewBranch", new { branchId = model.BranchId });
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var parishes = dbContext.Parishes.OrderBy(p => p.Name);

            ViewBag.parishes = parishes;

            return View(model);
        }

        public ActionResult EditBranch(int branchId = 0)
        {
            AgentBranch branch = dbContext.AgentBranches.Find(branchId);

            if(branch == null)
            {
                return HttpNotFound();
            }

            var parishes     = dbContext.Parishes.OrderBy(p => p.Name);
            ViewBag.parishes = parishes;

            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranch(AgentBranch model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewBranch", new { branchId = model.BranchId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var parishes = dbContext.Parishes.OrderBy(p => p.Name);
            ViewBag.parishes = parishes;

            return View(model);
        }

        public ActionResult ViewBranch(int branchId = 0)
        {
            var branch = dbContext.AgentBranches.Find(branchId);

            if(branch == null)
            {
                return HttpNotFound();
            }

            ViewBag.branch = branch;

            return View(branch);
        }

        public ActionResult ListBranches( int? page, int agentId = 0)
        {
            var agent = dbContext.Agents.Find(agentId);
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Branches);

            if(agent == null)
            {
                return HttpNotFound();
            }

            var branches = dbContext.AgentBranches
                .Where(b => b.AgentId == agentId)
                .OrderBy(b => b.Name)
                .ToPagedList(pageNumber, pageSize);            

            return View(branches);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }

    }
}
