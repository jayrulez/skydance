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
        private readonly IUserRepository userRepo;

        public TestController()
        {
            agentRepo = new AgentRepository();
            subcriberRepo = new SubscriberRepository();
            userRepo = new UserRepository();
        }

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


        public ActionResult GetUser()
        {
            IResponse<User> response = userRepo.GetUser(1 ,true);

            if (response.IsSuccessful)
            {
                ViewBag.User = response.Result;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();
        }


        public ActionResult GetUsers()
        {
            IResponse<User> response = new Response<User>();

            response = userRepo.GetUsers(2, 1, true);

            if (response.IsSuccessful)
            {
                ViewBag.Users = response.Results;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();
        }

        public ActionResult AddUsers()
        {
            IResponse<bool> response = new Response<bool>();
            var user = new BillBox.Models.User();
            user.UserLevelId = 1;
            user.AgentId = 1;
            user.AgentBranchId = 1;
            user.Name = "Test User 2";
            user.Username = "t2user";
            user.Password = "password";
            user.PasswordExpireAt = DateTime.Now;
            user.LoginStatus = 1;
            user.Designation = "Designation";
            user.AddressStreet = "Some street";
            user.AddressCity = "Kinsgton";
            user.ParishId = 7;
            user.ContactNumber = "123456798";
            user.EmailAddress = "test3@fu.jm";

            response = userRepo.AddUser(user);

            if (response.IsSuccessful)
            {
                ViewBag.Result = response.Results;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();
        }

        public ActionResult UpdateUser()
        {
            IResponse<bool> response = new Response<bool>();

            var user = new BillBox.Models.User();
            user.UserId = 3;
            user.UserLevelId = 1;
            user.AgentId = 1;
            user.AgentBranchId = 1;
            user.Name = "Test User 2";
            user.Username = "t2user";
            user.Password = "password";
            user.PasswordExpireAt = DateTime.Now;
            user.LoginStatus = 1;
            user.Designation = "Designation";
            user.AddressStreet = "Some street";
            user.AddressCity = "Kinsgton";
            user.ParishId = 7;
            user.ContactNumber = "123456798";
            user.EmailAddress = "test2@fu.jm";

            response = userRepo.UpdateUser(user);

            if (response.IsSuccessful)
            {
                ViewBag.Result = response.Results;
            }
            else
            {
                ViewBag.Error = response.Error;
            }

            return View();
        }
    }
}
