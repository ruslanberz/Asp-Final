﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using ASP_Final.Models;
using ASP_Final.DAL;
using ASP_Final.Helper;

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
            //List<Vw_rating> RatingList = new List<Vw_rating>();
     
            //int commentCounter=0;
            //double ratingBuffer = 0;
            //foreach (Place item in db.Places)
            //{
            //    List < Comment > temp = db.Comments.Where(x => x.PlaceId == item.Id).ToList();
            //    if (temp!=null)
            //    {
            //        foreach (var comment in temp)
            //        {
            //            ratingBuffer += comment.Rating;
            //            commentCounter++;

            //        }

            //        ratingBuffer /= commentCounter;

            //    }
            //    else
            //    {
            //        ratingBuffer = 0;
            //    }
            //    Vw_rating buf = new Vw_rating();
            //    buf.Ids = item.Id;
            //    buf.Rating = ratingBuffer;
            //    RatingList.Add(buf);
                
            //}

            //List<Vw_rating> Sorted = RatingList.OrderByDescending(x => x.Rating).ToList();
            //List<Vw_rating> Take3 = Sorted.Take(3).ToList();
            //foreach (var item in Take3)
            //{
            //    home.TopPlaces.Add(db.Places.Find(item.Ids));
            //}


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

        [HttpPost]

        public JsonResult Autocomplete(string Prefix)
        {

            var Countries = (from c in db.Categories
                             where c.Name.ToLower().StartsWith(Prefix.ToLower())
                             select new { c.Name, c.Id });
            return Json(Countries, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult AutocompleteCity(string Prefix)
        {

            var Cities = (from c in db.Cities
                             where c.Name.ToLower().StartsWith(Prefix.ToLower())
                             select new { c.Name, c.Id });
            return Json(Cities, JsonRequestBehavior.AllowGet);
        }

        [Auth]
        public ActionResult UserProfile()
        {
            int id = (int)Session["User"];
            VwProfile MyModel = new VwProfile();
            MyModel.Ratings = new List<double>();
            MyModel.Places= db.Places.Where(p => p.UserId == id).ToList();
            foreach (var item in MyModel.Places)
            {
                if (item.Comments.Count()>0)
                {
                    MyModel.Ratings.Add(item.Comments.Average(x => x.Rating));
                }
                else
                {
                    MyModel.Ratings.Add(0);
                }
             
       
            }
            User CurrentUser = db.Users.Find(id);
            if (MyModel!= null)
            {
                return View(MyModel);
            }
            else
            {
                return HttpNotFound();

            }


        }
    }
}