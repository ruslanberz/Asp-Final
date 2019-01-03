using ASP_Final.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.DAL;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ASP_Final.Controllers
{
    public class PlaceController : Controller
    {
        public readonly AspFinalContext db = new AspFinalContext();
        // GET: Place
        public ActionResult Index()
        {
            return View();
        }
        [Auth]
        public ActionResult Add() {

            VwAddPlace view = new VwAddPlace();
            view.Categories = db.Categories.ToList();
            view.Cities = db.Cities.ToList();
            return View(view);

        }


        [HttpPost]
        public JsonResult GetServices(int id)
        {
            if (id < 1)
            {
                return Json(new
                {
                    status = 404

                }, JsonRequestBehavior.AllowGet);
            }

            List<CategoryService> categories = db.CategoryServices.Where(x => x.CategoryId == id).ToList();
            List<int> ids = new List<int>();
            List<Service> services = new List<Service>();
            List<string> names = new List<string>();
            foreach (var item in categories)
            {
                ids.Add(item.ServiceId);
            }

            foreach (var item in ids)
            {
                if (db.Services.Find(item) != null)
                {
                    names.Add(db.Services.Find(item).Name);
                }
            }

            return Json(new
            {
                status = 200,
                list = ids,
                srvc = names

            }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]

        public JsonResult Create(Place place, HttpPostedFileBase[] files)
        {
            if (place!=null)
            {
                place.UserId = (int)(Session["User"]);
                db.Places.Add(place);
                db.SaveChanges();
            }



            if (files != null)
            {
                foreach (HttpPostedFileBase foto in files)
                {
                    if (foto.ContentLength>0)
                    {
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + foto.FileName;
                        string path = Path.Combine(Server.MapPath("~/Public/images/upload"), filename);
                        foto.SaveAs(path);
                        Photo photo = new Photo()
                        {
                            PhotoName = filename,
                            PlaceId = place.Id,

                        };
                        db.Photos.Add(photo);
                        db.SaveChanges();
                    }
                    
                }


                return Json(new
                {
                    status = 200,
                    url = "/home/index",
                    message = "Your place succesfully created. It will be available for all after moderator confirmation",
                    placeId = place.Id


                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 401,
                    message="Unable to save photos. Server could not get them. Please try to send again"

                }, JsonRequestBehavior.AllowGet);
            }




        }

        [HttpPost]
        public JsonResult Create2(int[] ser, List<Times> times,int placeId)
        {
            if (times != null)
            {
                foreach (var item in times)
                {
                    WorkHour temp = new WorkHour();
                    temp.OpenHour = TimeSpan.Parse(item.OpenHour);
                    temp.CloseHour = TimeSpan.Parse(item.CloseHour);
                    temp.WeekNo = item.WeekNo;
                    temp.PlaceId = placeId;
                    db.WorkHours.Add(temp);

                    db.SaveChanges();

                }
                if (ser!=null&&ser.Count()>0)
                {

                    foreach (var item in ser)
                    {
                        PlaceService ps = new PlaceService();
                        ps.PlaceId = placeId;
                        ps.ServiceId = item;
                        db.PlaceServices.Add(ps);
                        db.SaveChanges();
                    }
                }
                return Json(new
                {
                    status = 200,
                    url = "/home/index",
                    message="Place Succefssfully created. After moderator rewiew it will be available at the website."
                }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new
                {
                    status = 401,
                    message="Could not get work times,please fill form again"
                }, JsonRequestBehavior.AllowGet);

            }
            


        }


       

        public ActionResult Show (string Name,string City)

               
        {

            VwPlaceShow result = new VwPlaceShow();
            if (Name != null || City != null)
            {
                if (Name != "" && City == "")
                {
                    var places = db.Places.Include("City").Include("Category").Include("Photo").Where(x => x.Category.Name == Name);
                    var list = new List<Place>(places);
                    result.Places = list;

                }
                else if (City != "" && Name == "")
                {
                    var places = db.Places.Include("City").Include("Category").Include("Photos").Where(x => x.City.Name == City);
                    var list = new List<Place>(places);
                    result.Places = list;

                }
                else
                {
                    var places = db.Places.Include("City").Include("Category").Where(x => x.City.Name == City&&x.Category.Name==Name);
                    var list = new List<Place>(places);
                    result.Places = list;
                }
            }

            if (result.Places.Count() > 0 && result.Places.Count()!= null)
            {
                List<CategoryService> cs = new List<CategoryService>();

                cs = db.CategoryServices.Include("Service").Where(x => x.Category.Name == Name).ToList();
                result.Count= result.Places.Count();
                result.CategoryServices = cs;
                return View(result);

            }
            else
            {
                return new HttpNotFoundResult("Pleace specify correct search options");
            }
        }

    }


}