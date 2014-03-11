using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using System.Data.Objects;
using BillBox.Common;
using BillBox.Filters;

namespace BillBox.Controllers
{
    public class ReportController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        [RightFilter(RightName = "GENERATE_REPORT")]
        public ActionResult Collections()
        {            
            LoadLookupValues(ViewBag);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "GENERATE_REPORT")]
        public ActionResult Collections(CollectionsReportModel filter)
        {
            try
            {
                /*prepare collections in the db context*/
                var collections = dbContext.Bills
                .Where(bill => bill.Status == (int)BillStatus.Posted)
                .GroupJoin(dbContext.Payments, bill => bill.BillId, payment => payment.BillId, (bill, billGroup) => new CollectionsReportModel
                {
                    InvoiceNumber = bill.InvoiceNumber,
                    Date = bill.Date,
                    Amount = billGroup.Sum(p => p.Amount),
                    Agent = bill.Agent.Name,
                    Branch = bill.AgentBranch.Name,
                    Subscriber = bill.Subscriber.Name
                });

                /*prepare filters and then filter the collections in the context*/
                if (!string.IsNullOrEmpty(filter.DateRange))
                {
                    DateTime fromDate;
                    DateTime toDate;

                    /*check if a date range was was entered. indicated with a -*/
                    if (filter.DateRange.Contains('-'))
                    {
                        var dates = filter.DateRange.Split('-');

                        if (dates.Length == 2)
                        {
                            fromDate = StringToDate(dates[0]);
                            toDate = StringToDate(dates[1]).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                            collections = collections.Where(c => c.Date >= fromDate && c.Date <= toDate);

                        }
                        else
                        {
                            throw new Exception("Invalid date range");
                        }
                    }
                    else/*single date was entered*/
                    {
                        fromDate = StringToDate(filter.DateRange);
                        toDate = fromDate.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                        collections = collections.Where(c => c.Date >= fromDate && c.Date <= toDate);
                    }

                }

                /*filter the remainding params*/
                if (string.IsNullOrEmpty(filter.Subscriber) == false && filter.Subscriber.CompareTo("All") != 0)
                    collections = collections.Where(c => c.Subscriber == filter.Subscriber);

                if (string.IsNullOrEmpty(filter.Agent) == false && filter.Agent.CompareTo("All") != 0)
                    collections = collections.Where(c => c.Agent == filter.Agent);

                if (string.IsNullOrEmpty(filter.Branch) == false && filter.Branch.CompareTo("All") != 0)
                    collections = collections.Where(c => c.Branch == filter.Branch);

                if (string.IsNullOrEmpty(filter.Agent) == false && filter.Agent.CompareTo("All") != 0)
                    LoadLookupValues(ViewBag, filter.Agent);
                else
                    LoadLookupValues(ViewBag);
                
                /*Apply paging if necessary or just pull the data from the db and pass it to the viewbag*/
                if (filter.PageNumber > 1)
                    ViewBag.Collections = collections.ToList().Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
                else       
                    ViewBag.Collections = collections.ToList();                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(filter);
        }

        [HttpGet]
        public ActionResult Branches(string agent)
        {
            ViewBag.Branches = dbContext.AgentBranches.Where(b => b.Agent.Name == agent);
            return View("_Branches");
        }


        private DateTime StringToDate(string date)
        {
            string [] strDate;

            if (date.Contains('/'))
                strDate = date.Split('/');
            else
                throw new ArgumentOutOfRangeException("Invalid date format");

            try
            {
                /*format: yyyy/mm/dd*/
                var newDate = new DateTime(int.Parse(strDate[2]), int.Parse(strDate[0]), int.Parse(strDate[1]));
                return newDate;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Invalid date format");
            }
        }

        private void LoadLookupValues(dynamic dictionary, string agent = "null")
        {
            dictionary.Subscribers = dbContext.Subscribers.ToList();
            dictionary.Agents = dbContext.Agents.ToList();

            if (agent.CompareTo("null") != 0)
                dictionary.Branches = dbContext.AgentBranches.Where(b => b.Agent.Name == agent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }


    }
}
