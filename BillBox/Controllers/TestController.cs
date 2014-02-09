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

        private readonly IAgentRepository agentRepo;
        private readonly ISubscriberRepository subcriberRepo;

        public TestController()
        {
            agentRepo = new AgentRepository();
            subcriberRepo = new SubscriberRepository();
        }

        //
        // GET: /Test/        
        public ActionResult GetAgent()
        {
            IResponse<Agent> response = agentRepo.GetAgent(1);

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
            IResponse<AgentBranch> response = agentRepo.GetAgentBranches(1);

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
            IResponse<Agent> response = agentRepo.GetAgentWithBranches(1);

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


        public ActionResult GetSubscriber()
        {

            IResponse<Subscriber> response =  subcriberRepo.GetSubscriber(1);

            if (response.IsSuccessful)
            {
                ViewBag.Subscriber = response.Result;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();
        }

        
        public ActionResult GetSubscriberNames()
        {

            IResponse<Subscriber> response = subcriberRepo.GetSubscribersName(3, 2);


            if (response.IsSuccessful)
            {
                ViewBag.Subscribers = response.Results;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();

        }



    }
}
