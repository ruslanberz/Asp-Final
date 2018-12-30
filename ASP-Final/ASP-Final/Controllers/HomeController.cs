using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using ASP_Final.Models;
using ASP_Final.DAL;

namespace ASP_Final.Controllers
{
    public class HomeController : Controller


    {
        AspFinalContext db = new AspFinalContext();

        public ActionResult Index()
        {

            VwHomeIndex home = new VwHomeIndex();
            home.Categories = db.Categories.ToList();
            home.Places = db.Places.ToList();
            home.Cities = db.Cities.ToList(); //это надо поменять
            return View(home);
        }
        [HttpPost]
        public JsonResult Login(User user)

        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return Json(new
                {
                 status=401,
                 message="Email and password must be filled"
                },JsonRequestBehavior.AllowGet);
            }

            User chk =db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (chk!=null)
            {
                if (Crypto.VerifyHashedPassword(chk.Password,user.Password))
                {
                    Session["User"] = chk.Id;
                    return Json(new
                    {
                        status = 200,
                        url = "/home/index"
                    }, JsonRequestBehavior.AllowGet);
                }

            }

           
            return Json(new
            {

                status = 402,

               

            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Register (User user)

        {
            User check = db.Users.FirstOrDefault(x => x.Email == user.Email);
            if (string.IsNullOrEmpty(user.Email)|| string.IsNullOrEmpty(user.Fullname) || string.IsNullOrEmpty(user.Password))
            {
                return Json(new {
                    status=406,
                    message="You have to fill all required fields"


                },JsonRequestBehavior.AllowGet);
            }

            if (check!=null)
            {
                return Json(new
                {
                    status = 400,
                    message = "Email already in use"


                }, JsonRequestBehavior.AllowGet);
            }
            user.Password = Crypto.HashPassword(user.Password);
            db.Users.Add(user);
            db.SaveChanges();
            Session["User"] = user.Id;
            return Json(new
            {
                status = 200,
                message = "OK",
                url = "/home/index"


            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Generate()

        {
            return Content(Crypto.HashPassword("kamran"));
        }

        public ActionResult Logout() {

            Session["User"] = null;
            return RedirectToAction("index", "home");

        }
        [HttpPost]
        public JsonResult checklogin()

        {
            if (Session["User"]==null)
            {
                return Json(new
                {
                    status = 404

                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 200,
                    url = "/place/add"

                }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public JsonResult emailvalidation(string Email)
        {
            User check = db.Users.FirstOrDefault(x => x.Email == Email);

            if (check==null)
            {
                return Json(new
                {
                    valid = true,
                  

                },JsonRequestBehavior.AllowGet);

                

            }
            return Json(new
            {
                valid = false,
                message = "Email already in use"

            }, JsonRequestBehavior.AllowGet);
        }
      
    }
}