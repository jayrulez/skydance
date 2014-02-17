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
        // GET: /PaymentType/

        public ActionResult ListPaymentTypes(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentTypes);

            var paymentTypes = dbContext.PaymentTypes
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return View(paymentTypes);
        }

        public ActionResult CreatePaymentType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentType(PaymentType model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.PaymentTypes.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentType", new { paymentTypeId = model.PaymentTypeId });
                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult EditPaymentType(int paymentTypeId = 0)
        {
            PaymentType paymentType = dbContext.PaymentTypes.Find(paymentTypeId);

            if(paymentType == null)
            {
                return HttpNotFound();
            }

            ViewBag.paymentType = paymentType;

            return View(paymentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPaymentType(PaymentType model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentType", new { paymentTypeId = model.PaymentTypeId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult ViewPaymentType(int paymentTypeId = 0)
        {
            PaymentType paymentType = dbContext.PaymentTypes.Find(paymentTypeId);

            if(paymentType == null)
            {
                return HttpNotFound();
            }

            ViewBag.paymentType = paymentType;

            return View(paymentType);
        }

        public ActionResult AddPaymentTypeCaptureField(int paymentTypeId = 0)
        {
            var paymentType = dbContext.PaymentTypes.Find(paymentTypeId);

            if (paymentType == null)
            {
                return HttpNotFound();
            }

            PaymentTypeCaptureField captureField = new PaymentTypeCaptureField();

            captureField.PaymentTypeId = paymentType.PaymentTypeId;

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPaymentTypeCaptureField(PaymentTypeCaptureField model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.PaymentTypeCaptureFields.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentType", new { paymentTypeId = model.PaymentTypeId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult EditPaymentTypeCaptureField(int captureFieldId = 0)
        {
            var captureField = dbContext.PaymentTypeCaptureFields.Find(captureFieldId);

            if(captureField == null)
            {
                return HttpNotFound();
            }

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPaymentTypeCaptureField(PaymentTypeCaptureField model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentType", new { paymentTypeId = model.PaymentTypeId });

                }catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult OrderPaymentTypeCaptureFields(int? fieldUpId = 0, int? fieldDownId = 0)
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
