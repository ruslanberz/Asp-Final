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

        public ActionResult Index()
        {

            VwHomeIndex home = new VwHomeIndex();
            home.Categories = db.Categories.ToList();
            home.Places = db.Places.ToList();
            home.Cities = db.Cities.ToList();
            int count = db.Places.Count();
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

            }).OrderByDescending(z => z.Rating).Take(3).ToList();

            List<City> allC = db.Cities.ToList();
            List<VwTopCities> Cities = new List<VwTopCities>();
          

            foreach (var item in allC)
            {
                int CityCount = db.Places.Where(c => c.CityId == item.Id).Count();
                VwTopCities current = new VwTopCities();
                current.City = item;
                if (string.IsNullOrEmpty(item.Photo))
                {
                    current.City.Photo = "noimage.png"; 
                }
               

                current.PlaceCount = CityCount;
                Cities.Add(current);
            }
            List<VwTopCities> sort= Cities.OrderByDescending(o=>o.PlaceCount).Take(4).ToList();
            home.One = sort.ElementAt(0);
            home.Two = sort.ElementAt(1);
            home.Three = sort.ElementAt(2);
            home.Four = sort.ElementAt(3);
            home.LatestBlogs = db.Blogs.OrderByDescending(o => o.Date).Take(4).ToList();
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