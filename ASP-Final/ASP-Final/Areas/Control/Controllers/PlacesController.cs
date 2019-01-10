using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_Final.DAL;
using ASP_Final.Models;

namespace ASP_Final.Areas.Control.Controllers
{
    public class PlacesController : Controller
    {
        private AspFinalContext db = new AspFinalContext();

        // GET: Control/Places
        public ActionResult Index()
        {
            var places = db.Places.Include(p => p.Category).Include(p => p.City).Include(p => p.User);
            return View(places.ToList());
        }

        // GET: Control/Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Control/Places/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Control/Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,Name,Slogan,Description,Address,Phone,Website,CategoryId,CityId,UserId,Lat,Lng,Youtube,Facebook,Twitter,Keyword")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", place.CategoryId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", place.CityId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", place.UserId);
            return View(place);
        }

        // GET: Control/Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", place.CategoryId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", place.CityId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", place.UserId);
            return View(place);
        }

        // POST: Control/Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,Name,Slogan,Description,Address,Phone,Website,CategoryId,CityId,UserId,Lat,Lng,Youtube,Facebook,Twitter,Keyword")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", place.CategoryId);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", place.CityId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", place.UserId);
            return View(place);
        }

        // GET: Control/Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Control/Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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
