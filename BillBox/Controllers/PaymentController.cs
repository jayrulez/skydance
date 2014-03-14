using System;
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
            var captureFields = dbContext.CaptureFields.Where(cf => cf.SubscriberId == subscriberId).OrderBy(cf => cf.OrderNum);

            ViewBag.captureFields = captureFields;

            return PartialView("_CaptureFields");
        }

        public ActionResult GetPaymentMethodCaptureFields(int paymentMethodId)
        {
            var captureFields = dbContext.PaymentMethodCaptureFields.Where(cf => cf.PaymentMethodId == paymentMethodId).OrderBy(cf => cf.OrderNum);

            ViewBag.captureFields = captureFields;

            return PartialView("_PaymentMethodCaptureFields");
        }

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewBill()
        {
            var subscribers = dbContext.Subscribers;

            ViewBag.subscribers = subscribers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewBill(Bill model)
        {
            User user = Util.GetLoggedInUser();

            if(user == null || user.AgentBranch == null)
            {
                TempData["ErrorMessage"] = "Your account must associated with an agent to process payments.";

                return RedirectToAction("Error", "Default");
            }

            model.UserId        = user.UserId;
            model.AgentBranchId = user.AgentBranch.BranchId;
            model.AgentId       = user.AgentBranch.Agent.AgentId;
            model.Date          = DateTime.Now;
            model.InvoiceNumber = Util.GenerateInvoiceNumber();
            model.Status        = (int)BillStatus.Init;

            if (ModelState.IsValid)
            {
                try
                {
                    var subscriber = dbContext.Subscribers.Find(model.SubscriberId);

                    if(subscriber == null)
                    {
                        return HttpNotFound();
                    }

                    foreach(CaptureField captureField in subscriber.CaptureFields)
                    {
                        if (HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"] != null)
                        {
                            BillCaptureField billCaptureField = new BillCaptureField();
                            billCaptureField.CaptureFieldId   = captureField.CaptureFieldId;
                            billCaptureField.Value            = HttpContext.Request.Params["CaptureFields[" + captureField.Name + "]"];

                            model.BillCaptureFields.Add(billCaptureField);
                        }
                    }

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

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewPayment(int billId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult NewPayment(Payment model)
        {
            Bill bill = dbContext.Bills.Find(model.BillId);

            if (bill == null)
            {
                return HttpNotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    bill.Status                 = (int)BillStatus.Working;
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
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var paymentMethods = dbContext.PaymentMethods;

            ViewBag.paymentMethods = paymentMethods;

            return View(model);
        }

        [RightFilter(RightName = "VIEW_PAYMENT_HISTORY")]
        public ActionResult PaymentHistory(int? page, string period = "today")
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentHistory);

            var bills = dbContext.Bills.Where(b => b.Status == (int)BillStatus.Posted).OrderBy(b => b.BillId).ToPagedList(pageNumber, pageSize);  

            return View(bills);
        }

        [RightFilter(RightName = "VIEW_BILL")]
        public ActionResult ViewBill(int billId = 0)
        {
            Bill bill = dbContext.Bills.Find(billId);

            if(bill == null)
            {
                return HttpNotFound();
            }

            return View(bill);
        }

        [RightFilter(RightName = "PROCESS_PAYMENT")]
        public ActionResult PostBill(int billId = 0)
        {
            Bill bill = dbContext.Bills.Find(billId);

            if (bill == null)
            {
                return HttpNotFound();
            }

            bill.Status = (int)BillStatus.Posted;

            //TODO: Cannot post bill with 0 payments

            try
            {
                dbContext.Entry(bill).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("ViewBill", new { billId = bill.BillId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                return View("Error");
            }
        }

        [RightFilter(RightName = "UNPOST_BILL")]
        public ActionResult UnpostBill(int billId = 0)
        {
            Bill bill = dbContext.Bills.Find(billId);

            if (bill == null)
            {
                return HttpNotFound();
            }

            bill.Status = (int)BillStatus.Working;

            try
            {
                dbContext.Entry(bill).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("NewPayment", new { billId = bill.BillId });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                return View("Error");
            }
        }

        [RightFilter(RightName = "VIEW_RECEIPT")]
        public ActionResult ViewReceipt(int invoiceNumber = 0)
        {
            Bill bill = dbContext.Bills.FirstOrDefault(b => b.InvoiceNumber == invoiceNumber && b.Status == (int)BillStatus.Posted);

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