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
        public ActionResult NewPayment(Bill model)
        {
            var paymentMethods = dbContext.PaymentMethods;

            var subscribers = dbContext.Subscribers;

            ViewBag.paymentMethods = paymentMethods;
            ViewBag.subscribers  = subscribers;

            string ret = "";

            foreach (var key in HttpContext.Request.Params.AllKeys)
            {
                ret += "Key: " + key + " Val: " + HttpContext.Request.Params[key] + "\n\n";
            }

            return Content(ret);

            //return View(model);
        }

        public ActionResult PaymentHistory(int? page, string period = "today")
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentHistory);

            var bills = dbContext.Bills
                .OrderBy(b => b.BillId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);  

            return View(bills);
        }

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