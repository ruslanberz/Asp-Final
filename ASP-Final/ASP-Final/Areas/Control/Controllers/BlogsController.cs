using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_Final.DAL;
using ASP_Final.Helper;
using ASP_Final.Models;

namespace ASP_Final.Areas.Control.Controllers
{   [AdminAuth]
    public class BlogsController : Controller
    {
        private AspFinalContext db = new AspFinalContext();

        // GET: Control/Blogs
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        // GET: Control/Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Control/Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Control/Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Date,Categ,SmallImage,BigImage")] Blog blog, HttpPostedFileBase SmallImage, HttpPostedFileBase BigImage)
        {
            if (SmallImage!=null)
            {
                string filename1 = DateTime.Now.ToString("yyyyMMddHHmmssnnn")+SmallImage.FileName;
                string path = Path.Combine(Server.MapPath("~/Public/images/upload"),filename1);
                SmallImage.SaveAs(path);
                blog.SmallImage = filename1;


            }

            if (BigImage!=null)
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssnnn") + BigImage.FileName;
                string path = Path.Combine(Server.MapPath("~/Public/images/upload"), filename);
                BigImage.SaveAs(path);
                blog.SmallImage = filename;

            }
            else
            {

            }

            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Control/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Control/Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Date,Categ,SmallImage,BigImage")] Blog blog, HttpPostedFileBase SmallImage, HttpPostedFileBase BigImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                if (blog.BigImage==null)
                {
                    db.Entry(blog).Property(o => o.BigImage).IsModified = false;
                }
                else  
                {
                    if (SmallImage!=null)
                    {
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + SmallImage.FileName;
                        string path = Path.Combine(Server.MapPath("~/Public/images/upload"), filename);
                        SmallImage.SaveAs(path);
                        blog.SmallImage = filename;
                    }
                    else
                    {
                        blog.SmallImage = "пусто";
                    }
                   
                } 
             
                if (blog.SmallImage == null)
                {
                    db.Entry(blog).Property(o => o.SmallImage).IsModified = false;
                }
                else
                {
                    if (BigImage!=null)
                    {
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + BigImage.FileName;
                        string path = Path.Combine(Server.MapPath("~/Public/images/upload"), filename);
                        BigImage.SaveAs(path);
                        blog.BigImage = filename;
                    }
                    else
                    {
                        blog.BigImage = "пусто";
                    }
                   
                    
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Control/Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Control/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
