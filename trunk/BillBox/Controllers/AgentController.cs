using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using System.Data;

namespace BillBox.Controllers
{
    public class AgentController : Controller
    {
        private Entities dbContext = new Entities();
        //
        // GET: /Agent/

        public ActionResult Index(int? page)
        {
            var agents = dbContext.Agents.OrderBy(a => a.Name);

            var pageNumber = page ?? 1;
            
            ViewBag.agents = agents.ToPagedList(pageNumber, 25);

            return View();
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

            return View();
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
            var parishes = dbContext.Parishes.OrderBy(p => p.Name);

            ViewBag.parishes = parishes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(AgentBranch model)
        {
            var parishes = dbContext.Parishes.OrderBy(p => p.Name);

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

            return View();
        }

        public ActionResult ListBranches( int? page, int agentId = 0)
        {
            var agent = dbContext.Agents.Find(agentId);

            if(agent == null)
            {
                return HttpNotFound();
            }

            var branches = dbContext.AgentBranches.Where(b => b.AgentId == agentId).OrderBy(b => b.Name);

            var pageNumber   = page ?? 1;
            ViewBag.branches = branches.ToPagedList(pageNumber, 25);

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
