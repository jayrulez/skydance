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

        public ActionResult GetCaptureFields(int subscriberId)
        {
            try
            {
                var captureFields = dbContext.CaptureFields.Where(cf => cf.SubscriberId == subscriberId).OrderBy(cf => cf.OrderNum);

                ViewBag.captureFields = captureFields;

                return PartialView("_CaptureFields");
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        public ActionResult GetPaymentMethodCaptureFields(int paymentMethodId)
        {
            try
            {
                var captureFields = dbContext.PaymentMethodCaptureFields.Where(cf => cf.PaymentMethodId == paymentMethodId).OrderBy(cf => cf.OrderNum);

                ViewBag.captureFields = captureFields;

                return PartialView("_PaymentMethodCaptureFields");
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewBill()
        {
            try
            {
                var subscribers = dbContext.Subscribers;

                ViewBag.subscribers = subscribers;

                return View();
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewBill(Bill model)
        {
            try
            {
                User user = Util.GetLoggedInUser();

                if (user == null || user.AgentBranch == null)
                {
                    TempData["ErrorMessage"] = "Your account must associated with an agent to process payments.";

                    return RedirectToAction("Error", "Default");
                }
                else
                {
                    model.UserId = user.UserId;
                    model.AgentBranchId = user.AgentBranch.BranchId;
                    model.AgentId = user.AgentBranch.Agent.AgentId;
                    model.Date = DateTime.Now;
                    model.InvoiceNumber = Util.GenerateInvoiceNumber();
                    model.Status = (int)BillStatus.Init;

                    if (ModelState.IsValid)
                    {
                        var subscriber = dbContext.Subscribers.Find(model.SubscriberId);

                        if (subscriber == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            foreach (CaptureField captureField in subscriber.CaptureFields)
                            {
                                if (HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"] != null)
                                {
                                    BillCaptureField billCaptureField = new BillCaptureField();
                                    billCaptureField.CaptureFieldId = captureField.CaptureFieldId;
                                    billCaptureField.Value = HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"];

                                    model.BillCaptureFields.Add(billCaptureField);
                                }
                            }

                            dbContext.Bills.Add(model);

                            dbContext.SaveChanges();

                            return RedirectToAction("NewPayment", new { billId = model.BillId });
                        }
                    }
                    else
                    {
                        var subscribers = dbContext.Subscribers;

                        ViewBag.subscribers = subscribers;

                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewPayment(int billId)
        {
            try
            {
                Bill bill = dbContext.Bills.Find(billId);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                Payment payment = new Payment();

                payment.BillId = bill.BillId;

                var paymentMethods = dbContext.PaymentMethods;

                ViewBag.paymentMethods = paymentMethods;
                ViewBag.bill = bill;

                return View(payment);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewPayment(Payment model)
        {
            try
            {
                Bill bill = dbContext.Bills.Find(model.BillId);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                if (ModelState.IsValid)
                {
                    bill.Status = (int)BillStatus.Working;
                    dbContext.Entry(bill).State = EntityState.Modified;

                    var paymentMethod = dbContext.PaymentMethods.Find(model.PaymentMethodId);

                    if (paymentMethod == null)
                    {
                        return HttpNotFound();
                    }

                    foreach (PaymentMethodCaptureField captureField in paymentMethod.PaymentMethodCaptureFields)
                    {
                        if (HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"] != null)
                        {
                            PaymentPaymentMethodCaptureField paymentPaymentMethodCaptureField = new PaymentPaymentMethodCaptureField();
                            paymentPaymentMethodCaptureField.PaymentMethodCaptureFieldId = captureField.PaymentMethodCaptureFieldId;
                            paymentPaymentMethodCaptureField.Value = HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"];

                            model.PaymentPaymentMethodCaptureFields.Add(paymentPaymentMethodCaptureField);
                        }
                    }

                    dbContext.Payments.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("NewPayment", new { billId = model.BillId });
                }
                else
                {
                    var paymentMethods = dbContext.PaymentMethods;

                    ViewBag.paymentMethods = paymentMethods;

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "VIEW_PAYMENT_HISTORY")]
        public ActionResult PaymentHistory(int? page, string period = "today")
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = Util.GetPageSize(Common.PagedList.PaymentHistory);

                var bills = dbContext.Bills.Where(b => b.Status == (int)BillStatus.Posted).OrderBy(b => b.BillId).ToPagedList(pageNumber, pageSize);

                return View(bills);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
            
        }

        [RightFilter(RightName = "VIEW_BILL")]
        public ActionResult ViewBill(int billId = 0)
        {
            try
            {
                Bill bill = dbContext.Bills.Find(billId);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                return View(bill);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }            
        }

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult PostBill(int billId = 0)
        {  
            try
            {
                Bill bill = dbContext.Bills.Find(billId);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                bill.Status = (int)BillStatus.Posted;

                //TODO: Cannot post bill with 0 payments

                dbContext.Entry(bill).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("ViewBill", new { billId = bill.BillId });
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "UNPOST_BILL")]
        public ActionResult UnpostBill(int billId = 0)
        {
            try
            {
                Bill bill = dbContext.Bills.Find(billId);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                bill.Status = (int)BillStatus.Working;

                dbContext.Entry(bill).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("NewPayment", new { billId = bill.BillId });
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "VIEW_RECEIPT")]
        public ActionResult ViewReceipt(int invoiceNumber = 0)
        {
            try
            {
                Bill bill = dbContext.Bills.FirstOrDefault(b => b.InvoiceNumber == invoiceNumber && b.Status == (int)BillStatus.Posted);

                if (bill == null)
                {
                    return HttpNotFound();
                }

                return View(bill);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }

        private RedirectToRouteResult HandleErrorOnController(Exception exception)
        {
            string errorMessage;
            bool isHandled = Util.HandleException(exception, out errorMessage);

            if (isHandled)
                TempData["ErrorMessage"] = errorMessage;

            return RedirectToAction("Error", "Default", null);
        }

    }
}