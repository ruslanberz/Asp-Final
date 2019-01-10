using System;
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
        //  HERE IS METHOD TO VIEW HOMEPAGE, ALL STATISTICS HERE
        public ActionResult Index()
        {

            VwHomeIndex home = new VwHomeIndex();
            home.Categories = db.Categories.ToList();
            home.Places = db.Places.ToList();
            home.Cities = db.Cities.ToList();
            int count = db.Places.Count();  //HERE  ALL PLACESCOUNTIS GRABBED- TO SHOW ON HOMEPAGE
            home.AllPlaceCount = count;
            home.Zibiller = db.Places.Select(x => new VwHomeZibilleri
            {
                Id = x.Id,
                Title = x.Name,
                Slogan = x.Slogan,
                CategoryName = x.Category.Name,
                CategoryIcon = x.Category.Icon,
                CityName = x.City.Name,
                PhotoName = x.Photos.FirstOrDefault().PhotoName,
                CommentCount = x.Comments.Count(),
                Rating = x.Comments.Count()!=0? Math.Round(x.Comments.Average(y => y.Rating),1):0,
                WorkHours =x.WorkHours.ToList()

            }).OrderByDescending(z => z.Rating).Take(3).ToList(); //HERE IS LOCATED ALL NECESSARY DATA OF VIEW 

            List<City> allC = db.Cities.ToList();
            List<VwTopCities> Cities = new List<VwTopCities>();
          

            foreach (var item in allC)
            {   //COUNTING PLACES IN EACH CITY
                int CityCount = db.Places.Where(c => c.CityId == item.Id).Count();
                VwTopCities current = new VwTopCities();
                current.City = item;
                //IF THERE ISNO PHOTO OF CITY -ASSIGN A DEFAULT NULL -IMAGE)
                if (string.IsNullOrEmpty(item.Photo))
                {
                    current.City.Photo = "noimage.png"; 
                }
               

                current.PlaceCount = CityCount;
                Cities.Add(current);
            }
            //GRAB MOST FILLED FIRST 4 CITIESFOR STATISTIC


            //MUELLIM, BILIREM BUNU DAHA SELIQELI YAZMAQ OLARDI- SEBRIM CATMADI KI DUZELDIM  
            List<VwTopCities> sort= Cities.OrderByDescending(o=>o.PlaceCount).Take(4).ToList();
            home.One = sort.ElementAt(0);
            home.Two = sort.ElementAt(1);
            home.Three = sort.ElementAt(2);
            home.Four = sort.ElementAt(3);
            //BURA GEDER OLAN HISSEDEN SIHBET GEDIR 


            //ORDER CITIES IN RIGHT WAY
            home.LatestBlogs = db.Blogs.OrderByDescending(o => o.Date).Take(4).ToList();
            return View(home);
        }

        //LOGIN METHID
        [HttpPost]
        public JsonResult Login(User user)

        {   //DEFAULT CHECK
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
                else
                {
                    return Json(new
                    {
                        status = 401,
                        message = "Wrong usernme/password combination"
                    }, JsonRequestBehavior.AllowGet);
                }
            }

           
            return Json(new
            {

                status = 402,

               

            }, JsonRequestBehavior.AllowGet);
        }

        //REGISTER NEW USER
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
                url = "/home/index",

            }, JsonRequestBehavior.AllowGet);

        }

        //PASSWORD GENERATOR FOR INRERNAL USE
        public ActionResult Generate()

        {
            return Content(Crypto.HashPassword("RuslanBerz"));
        }

        //LOGOUT METHOD- NULLATES "User" ATTRIBUTE OF SESSION
        public ActionResult Logout() {

            Session["User"] = null;
            return RedirectToAction("index", "home");

        }

        //METHOD TO CHECK IS USER LOGGED ID
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

        //VALIDATION TO CHECK IF EMAIL IS ALREADY REGISTERED
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

        //LIVE AUTOCOMPLETE FOR CATEGORY
        [HttpPost]

        public JsonResult Autocomplete(string Prefix)
        {

            var Countries = (from c in db.Categories
                             where c.Name.ToLower().StartsWith(Prefix.ToLower())
                             select new { c.Name, c.Id });
            return Json(Countries, JsonRequestBehavior.AllowGet);
        }

        //LIVE AUTOCOMPLETE FOR CITY
        [HttpPost]

        public JsonResult AutocompleteCity(string Prefix)
        {

            var Cities = (from c in db.Cities
                             where c.Name.ToLower().StartsWith(Prefix.ToLower())
                             select new { c.Name, c.Id });
            return Json(Cities, JsonRequestBehavior.AllowGet);
        }

        //RENDERS ALL PLACES PUBLISHED BY USER. CAN SEE THERE IF PLACE WERE APPROVED BY ADMIN
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

        //REDIRECTION IF ANY ERROR OCCURED, 
        public ActionResult SearchError()
        {
            return View();
        }
    }
}