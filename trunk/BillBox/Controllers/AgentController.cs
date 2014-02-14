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

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
