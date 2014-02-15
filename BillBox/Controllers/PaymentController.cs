using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;

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

            ViewBag.SubscriberId = new SelectList(dbContext.Subscribers, "SubscriberId", "Name");

            ViewBag.paymentTypes = paymentTypes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPayment(NewPaymentModel model)
        {
            var paymentTypes = dbContext.PaymentTypes;

            ViewBag.SubscriberId = new SelectList(dbContext.Subscribers, "SubscriberId", "Name");

            ViewBag.paymentTypes = paymentTypes;
            return Content(HttpContext.Request.Params.ToString());
            //ViewBag.SubscriberId = new SelectList(db.Subscribers, "SubscriberId", "Name");
            //return View(model);
        }

        public ActionResult PaymentHistory(string period = "today")
        {
            var payments = dbContext.Payments.AsQueryable();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}