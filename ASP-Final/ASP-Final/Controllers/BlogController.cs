using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.DAL;

namespace ASP_Final.Controllers
{
    public class BlogController : Controller
    {

        AspFinalContext db = new AspFinalContext();
        // GET: Blog
        //BLOGUN ANA SEHIFESI
        public ActionResult Index()
        {
            List<Blog> blogs = db.Blogs.ToList();
            
            return View(blogs);
        }
        //SECILMISH BLOGUN DETAILS-A KECHID
        public ActionResult Details(int? Id)
        {
            if (Id != 0&&Id!=null)
            {
                Blog bg = db.Blogs.Find(Id);
                return View(bg);
            }

            return RedirectToAction("SearchError", "Home");

           
        }
    }
}