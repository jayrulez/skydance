﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using System.Data.Objects;
using BillBox.Common;
using BillBox.Filters;
using PagedList;

namespace BillBox.Controllers
{
    public class ReportController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        [RightFilter(RightName = "GENERATE_REPORT")]
        public ActionResult Collections(int? page, bool? generate, string dateRangeFrom, string dateRangeTo, string subscriber, string agent, string branch)
        {
            var filter = new CollectionsReportModel()
            {
                PageNumber = page ?? 1,
                DateRangeFrom = dateRangeFrom,
                DateRangeTo = dateRangeTo,
                Subscriber = subscriber,
                Agent = agent,
                Branch = branch
            };

            try
            {
                /*check if form is been loaded for the first time and just return the view with an empty filter*/
                if (generate == null || generate == false)
                {
                    LoadLookupValues(ViewBag);
                }
                else
                {
                    /*prepare collections in the db context*/
                    var collections = dbContext.Bills
                        .Where(bill => bill.Status == (int)BillStatus.Posted)
                        .GroupJoin(dbContext.Payments, bill => bill.BillId, payment => payment.BillId, (bill, billGroup) => new CollectionsReportModel
                        {
                            BillId = bill.BillId,
                            Date = bill.Date,
                            Amount = billGroup.Sum(p => p.Amount),
                            Agent = bill.Agent.Name,
                            Branch = bill.AgentBranch.Name,
                            Subscriber = bill.Subscriber.Name,
                            ProcessingFee = (double)(bill.ProcessingFee.HasValue ? bill.ProcessingFee : 0.00),
                            ProcessingFeeGCT = (double)(bill.ProcessingFeeGCT.HasValue ? bill.ProcessingFeeGCT : 0.00),
                            Commission = (double)(bill.Commission.HasValue ? bill.Commission : 0.00),
                            CommissionGCT = (double)(bill.CommissionGCT.HasValue ? bill.CommissionGCT : 0.00)
                        });

                    /*prepare filters and then filter the collections in the context*/
                    if (!string.IsNullOrEmpty(filter.DateRangeFrom))
                    {
                        DateTime fromDate;

                        fromDate = StringToDate(dateRangeFrom);
                        collections = collections.Where(c => c.Date >= fromDate);
                    }

                    if (!string.IsNullOrEmpty(filter.DateRangeTo))
                    {
                        DateTime toDate;

                        toDate = StringToDate(dateRangeTo);
                        toDate = toDate.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
                        collections = collections.Where(c => c.Date <= toDate);
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

                    filter.Count = collections.Count();

                    /*Apply paging and pass the result to the viewbag*/
                    ViewBag.Collections = collections.OrderByDescending(c => c.Date).ToPagedList(filter.PageNumber, filter.PageSize);
                }

                return View(filter);        

            }
            catch (Exception ex)
            {
                Util.HandleException(ex.GetBaseException());
                 return RedirectToAction("Error", "Default", null);
            }
        }


        [HttpGet]
        [RightFilter(RightName = "VIEW_AGENT_BRANCHES")]
        public ActionResult Branches(string agent)
        {
            try
            {
                ViewBag.Branches = dbContext.AgentBranches.Where(b => b.Agent.Name == agent);
                return View("_Branches");
            }
            catch (Exception ex)
            {
                Util.HandleException(ex.GetBaseException());
                 return RedirectToAction("Error", "Default", null);
            }
            
        }


        private DateTime StringToDate(string date)
        {
            string[] strDate;

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
