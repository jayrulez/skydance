using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using PagedList;

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

        public ActionResult GetPaymentTypeCaptureFields()
        {
            int paymentTypeId;

            if (!int.TryParse(HttpContext.Request.Params["paymentTypeId"], out paymentTypeId))
            {
                return Content("Error");
            }

            var captureFields = dbContext.PaymentTypeCaptureFields.Where(cf => cf.PaymentTypeId == paymentTypeId).OrderBy(cf => cf.OrderNum);

            ViewBag.captureFields = captureFields;

            return PartialView("_PaymentTypeCaptureFields");
        }

        public ActionResult NewPayment()
        {
            var paymentTypes = dbContext.PaymentTypes;

            var subscribers = dbContext.Subscribers;

            ViewBag.paymentTypes = paymentTypes;
            ViewBag.subscribers = subscribers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPayment(Payment model)
        {
            var paymentTypes = dbContext.PaymentTypes;

            var subscribers = dbContext.Subscribers;

            ViewBag.paymentTypes = paymentTypes;
            ViewBag.subscribers  = subscribers;

            return Content(HttpContext.Request.Params.ToString());

            //return View(model);
        }

        public ActionResult PaymentHistory(int? page, string period = "today")
        {
            var payments = dbContext.Payments.OrderBy(p => p.PaymentId);

            var pageNumber = page ?? 1;

            ViewBag.payments = payments.ToPagedList(pageNumber, 25);

            return View();
        }

        public ActionResult ViewPayment(int paymentId = 0)
        {
            Payment payment = dbContext.Payments.Find(paymentId);

            if(payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}