using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;
using BillBox.Common;
using BillBox.Filters;

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
        [RightFilter(RightName = "VIEW_PAYMENT_METHODS")]
        public ActionResult ListPaymentMethods(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentMethods);

            var paymentMethods = dbContext.PaymentMethods
                .OrderBy(p => p.Name)
                .ToPagedList(pageNumber, pageSize);

            return View(paymentMethods);
        }

        [RightFilter(RightName = "CREATE_PAYMENT_METHOD")]
        public ActionResult CreatePaymentMethod()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_PAYMENT_METHOD")]
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

        [RightFilter(RightName = "EDIT_PAYMENT_METHOD")]
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
        [RightFilter(RightName = "EDIT_PAYMENT_METHOD")]
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

        [RightFilter(RightName = "VIEW_PAYMENT_METHOD")]
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

        [RightFilter(RightName = "CREATE_PAYMENT_METHOD_CAPTURE_FIELD")]
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
        [RightFilter(RightName = "CREATE_PAYMENT_METHOD_CAPTURE_FIELD")]
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

        [RightFilter(RightName = "EDIT_PAYMENT_METHOD_CAPTURE_FIELD")]
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
        [RightFilter(RightName = "EDIT_PAYMENT_METHOD_CAPTURE_FIELD")]
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

        [RightFilter(RightName = "ORDER_PAYMENT_METHOD_CAPTURE_FIELDS")]
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

        [RightFilter(RightName = "ASSIGN_USER_RIGHTS")]
        public ActionResult UserLevelRights(int levelId = 0)
        {
            var userLevels = dbContext.UserLevels;

            ViewBag.userLevels = userLevels;

            ViewBag.levelId = levelId;

            return View();
        }

        [RightFilter(RightName = "ASSIGN_USER_RIGHTS")]
        [HttpPost]
        public ActionResult UserLevelRights()
        {
            int levelId;

            if (!int.TryParse(HttpContext.Request.Params["levelId"], out levelId))
            {
                ViewBag.errorMessage = "User level not selected.";
            }

            var level = dbContext.UserLevels.Find(levelId);

            if(level == null)
            {
                ViewBag.errorMessage = "Selected User Level is invalid.";
            }

            var userRights = dbContext.UserRights;

            foreach(UserRight right in userRights)
            {
                if (HttpContext.Request.Params["right[" + right.Name + "]"] != null)
                {
                    var checkbox = HttpContext.Request.Params["right[" + right.Name + "]"];

                    if (checkbox.Contains("true"))
                    {
                        if(!level.HasRight(right.Name))
                        {
                            level.UserRights.Add(right);

                            dbContext.Entry(level).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        if(level.HasRight(right.Name))
                        {
                            level.UserRights.Remove(right);

                            dbContext.Entry(level).State = EntityState.Modified;
                        }
                    }
                }
            }

            if(dbContext.Entry(level).State == EntityState.Modified)
            {
                try
                {
                    dbContext.SaveChanges();

                    return RedirectToAction("UserLevelRights", new { levelId = level.LevelId });
                }
                catch (Exception ex)
                {
                    ViewBag.errorMessage = ex.Message;
                }
            }

            var userLevels = dbContext.UserLevels;
            ViewBag.userLevels = userLevels;
            ViewBag.levelId = levelId;

            return View();
        }

        public ActionResult UserLevelRightsFields(int levelId)
        {
            var level = dbContext.UserLevels.Find(levelId);

            if(level == null)
            {
                return Content("");
            }

            var rights = dbContext.UserRights;

            ViewBag.rights = rights;
            ViewBag.userLevel = level;

            return PartialView("_UserLevelRightsFields");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
