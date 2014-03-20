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

        [RightFilter(RightName = "VIEW_SETTINGS")]
        public ActionResult Index()
        {
            var settings = dbContext.Settings;

            return View(settings);
        }

        [RightFilter(RightName = "CHANGE_SETTINGS")]
        public ActionResult SaveSettings()
        {
            return View();
        }


        [RightFilter(RightName = "VIEW_PAYMENT_METHODS")]
        public ActionResult ListPaymentMethods(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.PaymentMethods);

            try
            {
                var paymentMethods = dbContext.PaymentMethods
                    .OrderBy(p => p.Name)
                    .ToPagedList(pageNumber, pageSize);

                return View(paymentMethods);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.PaymentMethods.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_PAYMENT_METHOD")]
        public ActionResult EditPaymentMethod(int paymentMethodId = 0)
        {
            try
            {
                PaymentMethod paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

                if (paymentMethod == null)
                {
                    return HttpNotFound();
                }

                ViewBag.paymentMethod = paymentMethod;

                return View(paymentMethod);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_PAYMENT_METHOD")]
        public ActionResult EditPaymentMethod(PaymentMethod model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [RightFilter(RightName = "VIEW_PAYMENT_METHOD")]
        public ActionResult ViewPaymentMethod(int paymentMethodId = 0)
        {
            try
            {
                PaymentMethod paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

                if (paymentMethod == null)
                {
                    return HttpNotFound();
                }

                ViewBag.paymentMethod = paymentMethod;

                return View(paymentMethod);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [RightFilter(RightName = "CREATE_PAYMENT_METHOD_CAPTURE_FIELD")]
        public ActionResult AddPaymentMethodCaptureField(int paymentMethodId = 0)
        {
            try
            {
                var paymentMethod = dbContext.PaymentMethods.Find(paymentMethodId);

                if (paymentMethod == null)
                {
                    return HttpNotFound();
                }

                PaymentMethodCaptureField captureField = new PaymentMethodCaptureField();

                captureField.PaymentMethodId = paymentMethod.PaymentMethodId;

                ViewBag.paymentMethod = paymentMethod;

                return View(captureField);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_PAYMENT_METHOD_CAPTURE_FIELD")]
        public ActionResult AddPaymentMethodCaptureField(PaymentMethodCaptureField model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.PaymentMethodCaptureFields.Add(model);

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_PAYMENT_METHOD_CAPTURE_FIELD")]
        public ActionResult EditPaymentMethodCaptureField(int captureFieldId = 0)
        {
            try
            {
                var captureField = dbContext.PaymentMethodCaptureFields.Find(captureFieldId);

                if (captureField == null)
                {
                    return HttpNotFound();
                }

                ViewBag.paymenMethod = captureField.PaymentMethod;
                return View(captureField);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_PAYMENT_METHOD_CAPTURE_FIELD")]
        public ActionResult EditPaymentMethodCaptureField(PaymentMethodCaptureField model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Entry(model).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("ViewPaymentMethod", new { paymentMethodId = model.PaymentMethodId });
                }

                ViewBag.paymentMethod = model.PaymentMethod;

                return View(model);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "ORDER_PAYMENT_METHOD_CAPTURE_FIELDS")]
        public ActionResult OrderPaymentMethodCaptureFields(int? fieldUpId = 0, int? fieldDownId = 0)
        {
            try
            {
                var fieldUp = dbContext.CaptureFields.Find(fieldUpId);

                if (fieldUp == null)
                {
                    return Content("Error_field_up_not_found");
                }

                var fieldDown = dbContext.CaptureFields.Find(fieldDownId);

                if (fieldDown == null)
                {
                    return Content("Error_field_down_not_found");
                }

                int fieldUpPos = fieldUp.OrderNum.Value;

                fieldUp.OrderNum = fieldDown.OrderNum;

                fieldDown.OrderNum = fieldUpPos;

                dbContext.SaveChanges();

                return Content("Success");
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "ASSIGN_USER_RIGHTS")]
        public ActionResult UserLevelRights(int levelId = 0)
        {
            try
            {
                var userLevels = dbContext.UserLevels;

                ViewBag.userLevels = userLevels;

                ViewBag.levelId = levelId;

                return View();
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [RightFilter(RightName = "ASSIGN_USER_RIGHTS")]
        [HttpPost]
        public ActionResult UserLevelRights()
        {
            try
            {
                int levelId;

                if (!int.TryParse(HttpContext.Request.Params["levelId"], out levelId))
                {
                    ViewBag.errorMessage = "User level not selected.";
                }

                var level = dbContext.UserLevels.Find(levelId);

                if (level == null)
                {
                    ViewBag.errorMessage = "Selected User Level is invalid.";
                }

                var userRights = dbContext.UserRights;

                foreach (UserRight right in userRights)
                {
                    if (HttpContext.Request.Params["right[" + right.Name + "]"] != null)
                    {
                        var checkbox = HttpContext.Request.Params["right[" + right.Name + "]"];

                        if (checkbox.Contains("true"))
                        {
                            if (!level.HasRight(right.Name))
                            {
                                level.UserRights.Add(right);

                                dbContext.Entry(level).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            if (level.HasRight(right.Name))
                            {
                                level.UserRights.Remove(right);

                                dbContext.Entry(level).State = EntityState.Modified;
                            }
                        }
                    }
                }

                if (dbContext.Entry(level).State == EntityState.Modified)
                {
                    dbContext.SaveChanges();

                    return RedirectToAction("UserLevelRights", new { levelId = level.LevelId });
                }

                var userLevels = dbContext.UserLevels;
                ViewBag.userLevels = userLevels;
                ViewBag.levelId = levelId;

                return View();
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        public ActionResult UserLevelRightsFields(int levelId)
        {
            try
            {
                var level = dbContext.UserLevels.Find(levelId);

                if (level == null)
                {
                    return Content("");
                }

                var rights = dbContext.UserRights;

                ViewBag.rights = rights;
                ViewBag.userLevel = level;

                return PartialView("_UserLevelRightsFields");
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
