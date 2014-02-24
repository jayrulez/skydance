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
            ViewBag.Parishes = dbContext.Parishes;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_SUBSCRIBER")]
        public ActionResult Create(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Subscribers.Add(subscriber);
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = subscriber.SubscriberId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>();

            return View(subscriber);
        }
        
        [HttpGet]
        [RightFilter(RightName = "VIEW_SUBSCRIBERS")]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = Util.GetPageSize(Common.PagedList.Subscribers);

            var subscribers = dbContext.Subscribers
                .Include(s => s.Parish)
                .OrderBy(s => s.Name)
                .ToPagedList(pageNumber, pageSize);
            

            return View(subscribers);
        }

        [RightFilter(RightName = "VIEW_SUBSCRIBER")]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var subscriber = dbContext.Subscribers.Find(id);

            if (subscriber == null)            
                return HttpNotFound();
            

            return View(subscriber);
        }

        [RightFilter(RightName = "EDIT_SUBSCRIBER")]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var subscriber = dbContext.Subscribers.Find(id);

            if (subscriber == null)            
                return HttpNotFound();            

            ViewBag.Parishes = dbContext.Parishes;

            return View(subscriber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_SUBSCRIBER")]
        public ActionResult Edit(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(subscriber).State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = subscriber.SubscriberId});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }                
            }

            ViewBag.Parishes = dbContext.Parishes;
            return View(subscriber);
        }

        [RightFilter(RightName = "EDIT_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult EditCaptureField(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var captureField = dbContext.CaptureFields.Find(id);

            if (captureField == null)
                return HttpNotFound();

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "EDIT_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult EditCaptureField(CaptureField captureField)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Entry(captureField).State = EntityState.Modified;
                    
                    dbContext.SaveChanges();

                    return RedirectToAction("Details", "Subscriber", new { id = captureField.SubscriberId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(captureField);
        }

        [RightFilter(RightName = "CREATE_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult CreateCaptureField(int id = 0)
        {
            if (id == 0)
                return HttpNotFound();

            var subscriber  = dbContext.Subscribers.Find(id); 

            if (subscriber == null)
                return HttpNotFound();

            var captureField = new CaptureField();

            captureField.SubscriberId = subscriber.SubscriberId;

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RightFilter(RightName = "CREATE_SUBSCRIBER_CAPTURE_FIELD")]
        public ActionResult CreateCaptureField(CaptureField captureField)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.CaptureFields.Add(captureField);

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = captureField.SubscriberId });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(captureField);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }

    }
}
