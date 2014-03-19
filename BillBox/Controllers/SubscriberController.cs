using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using BillBox.Common;
using System.Data.Entity;
using PagedList;
using System.Data;
using BillBox.Filters;

namespace BillBox.Controllers
{
    [Authorize]
    public class SubscriberController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        [RightFilter(RightName = "CREATE_SUBSCRIBER")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Parishes = dbContext.Parishes;

                return View();
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_SUBSCRIBER")]
        public ActionResult Create(Subscriber subscriber)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Subscribers.Add(subscriber);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = subscriber.SubscriberId });
                }
                else
                {
                    ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>();

                    return View(subscriber);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpGet]
        [RightFilter(RightName = "VIEW_SUBSCRIBERS")]
        public ActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var pageSize = Util.GetPageSize(Common.PagedList.Subscribers);

                var totalRecords = dbContext.Subscribers.Count();
                var subscribers = dbContext.Subscribers
                    .Include(s => s.Parish)
                    .OrderBy(s => s.Name)
                    .ToPagedList(pageNumber, pageSize);

                Util.PreparePagerInfo(ControllerContext.RequestContext, ViewBag, "Index", pageNumber, pageSize, totalRecords);

                return View(subscribers);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "VIEW_SUBSCRIBER")]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var subscriber = dbContext.Subscribers.Find(id);

                if (subscriber == null)
                    return HttpNotFound();

                return View(subscriber);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_SUBSCRIBER")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var subscriber = dbContext.Subscribers.Find(id);

                if (subscriber == null)
                    return HttpNotFound();

                ViewBag.Parishes = dbContext.Parishes;

                return View(subscriber);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_SUBSCRIBER")]
        public ActionResult Edit(Subscriber subscriber)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Entry(subscriber).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = subscriber.SubscriberId });
                }
                else
                {
                    ViewBag.Parishes = dbContext.Parishes;
                    return View(subscriber);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "EDIT_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult EditCaptureField(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var captureField = dbContext.CaptureFields.Find(id);

                if (captureField == null)
                    return HttpNotFound();

                return View(captureField);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult EditCaptureField(CaptureField captureField)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Entry(captureField).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", "Subscriber", new { id = captureField.SubscriberId });
                }
                else
                {
                    return View(captureField);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }
        }

        [RightFilter(RightName = "CREATE_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult CreateCaptureField(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            try
            {
                var subscriber = dbContext.Subscribers.Find(id);

                if (subscriber == null)
                    return HttpNotFound();

                var captureField = new CaptureField();

                captureField.SubscriberId = subscriber.SubscriberId;

                return View(captureField);
            }
            catch (Exception ex)
            {
                return HandleErrorOnController(ex.GetBaseException());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult CreateCaptureField(CaptureField captureField)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.CaptureFields.Add(captureField);

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = captureField.SubscriberId });
                }
                else
                {
                    return View(captureField);
                }
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
