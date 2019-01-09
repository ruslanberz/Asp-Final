using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_Final.DAL;
using ASP_Final.Helper;
using ASP_Final.Models;

namespace ASP_Final.Areas.Control.Controllers
{
    [AdminAuth]
    public class CategoryServicesController : Controller
    {
        private AspFinalContext db = new AspFinalContext();

        // GET: Control/CategoryServices
        public ActionResult Index()
        {
            var categoryServices = db.CategoryServices.Include(c => c.Category).Include(c => c.Service);
            return View(categoryServices.ToList());
        }

        // GET: Control/CategoryServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            return View(categoryService);
        }

        // GET: Control/CategoryServices/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            return View();
        }

        // POST: Control/CategoryServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,ServiceId")] CategoryService categoryService)
        {
            if (ModelState.IsValid)
            {
                db.CategoryServices.Add(categoryService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryService.CategoryId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", categoryService.ServiceId);
            return View(categoryService);
        }

        // GET: Control/CategoryServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryService.CategoryId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", categoryService.ServiceId);
            return View(categoryService);
        }

        // POST: Control/CategoryServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,ServiceId")] CategoryService categoryService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", categoryService.CategoryId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", categoryService.ServiceId);
            return View(categoryService);
        }

        // GET: Control/CategoryServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryService categoryService = db.CategoryServices.Find(id);
            if (categoryService == null)
            {
                return HttpNotFound();
            }
            return View(categoryService);
        }

        // POST: Control/CategoryServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryService categoryService = db.CategoryServices.Find(id);
            db.CategoryServices.Remove(categoryService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
