using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using BillBox.Common;
using BillBox.Models.Repository;

namespace BillBox.Controllers
{
    public class TestController : Controller
    {

        private readonly IAgentRepository repository;

        public TestController()
        {
            repository = new AgentRepository();
        }

        //
        // GET: /Test/        
        public ActionResult GetAgent()
        {
            IResponse<Agent> response = repository.GetAgent(1);

            if (response.IsSuccessful)
            {
                ViewBag.Agent = response.Result;
                
            }
            else
            {
                ViewBag.Error = response.Error.ToString();
            }
            return View();
        }

        public ActionResult GetAgentBranches()
        {
            IResponse<AgentBranch> response = repository.GetAgentBranches(1);

            if (response.IsSuccessful)
            {
                ViewBag.AgentBranches = response.Results;
            }
            else
            {
                ViewBag.Error = response.Error.ToString();
            }
            return View();
        }


        public ActionResult GetAgentAndBranches()
        {
            IResponse<Agent> response = repository.GetAgentWithBranches(1);

            if (response.IsSuccessful)
            {
                ViewBag.Agent = response.Result;

            }
            else
            {
                ViewBag.Error = response.Error.ToString();
            }
            return View();
        }

    }
}
