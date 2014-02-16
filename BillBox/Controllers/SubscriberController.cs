using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillBox.Models;
using System.Data.Entity;
using PagedList;
using System.Data;

namespace BillBox.Controllers
{
    [Authorize]
    public class SubscriberController : Controller
    {
        private Entities dbContext = new Entities();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Parishes = dbContext.Parishes;

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Index(int? page)
        {
            var subscribers = dbContext.Subscribers.Include(s => s.Parish).OrderBy(s => s.Name);

            var pageNumber = page ?? 1;

            ViewBag.Subscribers = subscribers.ToPagedList(pageNumber, 25);

            return View();
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            var subscriber = dbContext.Subscribers.Find(id);

            if (subscriber == null)
            {
                return HttpNotFound();
            }

            return View(subscriber);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var subscriber = dbContext.Subscribers.Find(id);

            if (subscriber == null)
            {
                return HttpNotFound();
            }

            ViewBag.Parishes = dbContext.Parishes.AsEnumerable<BillBox.Models.Parish>();

            return View(subscriber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entry = dbContext.Entry(subscriber);
                    entry.State = EntityState.Modified;

                    dbContext.SaveChanges();

                    return RedirectToAction("Details", new { id = subscriber.SubscriberId});
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
        public ActionResult EditCaptureField(int id = 0)
        {
            var captureField = dbContext.CaptureFields.Find(id);

            if (captureField == null)
            {
                return HttpNotFound();
            }

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCaptureField(CaptureField captureField)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entry = dbContext.Entry(captureField);
                    entry.State = EntityState.Modified;
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

        [HttpGet]
        public ActionResult CreateCaptureField(int id = 0)
        {
            var subscriber = dbContext.Subscribers.Find(id);

            if (subscriber == null)
            {
                return HttpNotFound();
            }

            CaptureField captureField = new CaptureField();            

            captureField.SubscriberId = subscriber.SubscriberId;

            return View(captureField);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
