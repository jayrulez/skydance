﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Common;
using BillBox.Models;
using PagedList;
using System.Data.Common;
using BillBox.Filters;

namespace BillBox.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private Entities dbContext = new Entities();

        public ActionResult GetCaptureFields()
        {
            int subscriberId;

            if(!int.TryParse(HttpContext.Request.Params["subscriberId"], out subscriberId)) {
                return Content("Error");
            }

            var captureFields = dbContext.CaptureFields.Where(cf => cf.SubscriberId == subscriberId).OrderBy(cf => cf.OrderNum);

            ViewBag.captureFields = captureFields;

            return PartialView("_CaptureFields");
        }

        public ActionResult GetPaymentMethodCaptureFields()
        {
            int paymentMethodId;

            if (!int.TryParse(HttpContext.Request.Params["paymentMethodId"], out paymentMethodId))
            {
                return Content("Error");
            }

            var captureFields = dbContext.PaymentMethodCaptureFields.Where(cf => cf.PaymentMethodId == paymentMethodId).OrderBy(cf => cf.OrderNum);

            ViewBag.captureFields = captureFields;

            return PartialView("_PaymentMethodCaptureFields");
        }

        public ActionResult NewPayment()
        {
            var paymentMethods = dbContext.PaymentMethods;

            var subscribers = dbContext.Subscribers;

            ViewBag.paymentMethods = paymentMethods;
            ViewBag.subscribers = subscribers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter]
        public ActionResult NewPayment(Bill model)
        {
            User user = Util.GetLoggedInUser();

            if(user == null || user.AgentBranch == null)
            {
                throw new HttpException(403, "You are not authorized to view this page.");
            }

            model.UserId        = user.UserId;
            model.AgentBranchId = user.AgentBranch.BranchId;
            model.AgentId       = user.AgentBranch.Agent.AgentId;
            model.Date          = DateTime.Now;
            model.InvoiceNumber = Util.GenerateInvoiceNumber();
            model.Status        = 1;

            if(ModelState.IsValid)
            {
                try
                {                    
                    dbContext.Bills.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("NewPayment", new { billId = model.BillId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var subscribers = dbContext.Subscribers;

            ViewBag.subscribers  = subscribers;

            return View(model);
        }

        public ActionResult NewPayment(int billId)
        {
            Bill bill = dbContext.Bills.Find(billId);

            if (bill == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        public ActionResult NewPayment(Payment model)
        {
            if(ModelState.IsValid)
            {
                dbContext.Payments.Add(model);

                dbContext.SaveChanges();

                return RedirectToAction("NewPayment", new { billId = model.BillId });
            }

            return View(model);
        }

        public ActionResult PaymentHistory(int? page, string period = "today")
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentHistory);

            var bills = dbContext.Bills.ToPagedList(pageNumber, pageSize);  

            return View(bills);
        }

        [RightFilter(RightName = "ViewPaymentHistory")]
        public ActionResult ViewBill(int billId = 0)
        {
            Bill bill = dbContext.Bills.Find(billId);

            if(bill == null)
            {
                return HttpNotFound();
            }

            return View(bill);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}