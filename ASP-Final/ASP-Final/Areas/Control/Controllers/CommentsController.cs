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
{   [AdminAuth]
    public class CommentsController : Controller
    {
        private AspFinalContext db = new AspFinalContext();

        // GET: Control/Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Place).Include(c => c.User).Include(c=>c.CommentPhotos);
            return View(comments.ToList());
        }

        // GET: Control/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Control/Comments/Create
        public ActionResult Create()
        {
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname");
            return View();
        }

        // POST: Control/Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlaceId,UserId,Rating,Text,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", comment.PlaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // GET: Control/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", comment.PlaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // POST: Control/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlaceId,UserId,Rating,Text,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", comment.PlaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // GET: Control/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Include(c => c.Place).Include(c => c.User).FirstOrDefault(x=>x.Id==id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Name", comment.PlaceId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Fullname", comment.UserId);
            return View(comment);
        }

        // POST: Control/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
