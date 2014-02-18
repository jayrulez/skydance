using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;
using BillBox.Common;

namespace BillBox.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private Entities dbContext = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PaymentMethod/

        public ActionResult ListPaymentMethods(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentMethods);

            var paymentMethods = dbContext.PaymentMethods
                .OrderBy(p => p.Name)
                .ToPagedList(pageNumber, pageSize);

            return View(paymentMethods);
        }

        public ActionResult CreatePaymentMethod()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentMethod(PaymentMethod model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.PaymentMethods.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult EditPaymentMethod(int paymentMethodId = 0)
        {
            PaymentMethod paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

            if(paymentMethod == null)
            {
                return HttpNotFound();
            }

            ViewBag.paymentMethod = paymentMethod;

            return View(paymentMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPaymentMethod(PaymentMethod model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult ViewPaymentMethod(int paymentMethodId = 0)
        {
            PaymentMethod paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

            if(paymentMethod == null)
            {
                return HttpNotFound();
            }

            ViewBag.paymentMethod = paymentMethod;

            return View(paymentMethod);
        }

        public ActionResult AddPaymentMethodCaptureField(int paymentMethodId = 0)
        {
            var paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            PaymentMethodCaptureField captureField = new PaymentMethodCaptureField();

            captureField.PaymentMethodId = paymentMethod.PaymentMethodId;

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPaymentMethodCaptureField(PaymentMethodCaptureField model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.PaymentMethodCaptureFields.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult EditPaymentMethodCaptureField(int captureFieldId = 0)
        {
            var captureField = dbContext.PaymentMethodCaptureFields.Find(captureFieldId);

            if(captureField == null)
            {
                return HttpNotFound();
            }

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPaymentMethodCaptureField(PaymentMethodCaptureField model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult OrderPaymentMethodCaptureFields(int? fieldUpId = 0, int? fieldDownId = 0)
        {
            var fieldUp   = dbContext.CaptureFields.Find(fieldUpId);

            if(fieldUp == null)
            {
                return Content("Error_field_up_not_found");
            }

            var fieldDown = dbContext.CaptureFields.Find(fieldDownId);

            if (fieldDown == null)
            {
                return Content("Error_field_down_not_found");
            }

            int fieldUpPos = fieldUp.OrderNum;

            fieldUp.OrderNum = fieldDown.OrderNum;

            fieldDown.OrderNum = fieldUpPos;

            try
            {
                dbContext.SaveChanges();

                return Content("Success");
            }catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
