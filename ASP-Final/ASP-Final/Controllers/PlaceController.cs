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

            if (place.Name==null)
            {
                return Json(new
                {
                    status = 401,
                    message = "You have to specify the name of place!"

                }, JsonRequestBehavior.AllowGet);
            }
            else if (place.CategoryId<=0)
            {
                return Json(new
                {
                    status = 401,
                    message = "You have to specify the category of place!"

                }, JsonRequestBehavior.AllowGet);
            }

          
            else
            {
                place.UserId = (int)(Session["User"]);
                db.Places.Add(place);
                db.SaveChanges();
            }



            if (files != null)
            {
                foreach (HttpPostedFileBase foto in files)
                {
                    if (foto!= null&&foto.ContentLength > 0)
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
            result.IsOpenedNow = new List<int>();
            result.IsAllSame = true;
            result.Services = db.Services.ToList();
            List<CategoryService> cs = new List<CategoryService>();
            //Bu massive Name gelmeyende Category id-leri yigilir. Show html-de butun muvafiq servisleri getirmek uchun
          List<int> ArrayOfPlaceCategories= new List<int>();
            if (Name != null || City != null)
            {
                if (Name != "" && City == "")
                {
                    var places = db.Places.Include("City").Include("Category").Include("Photos").Include("PlaceServices").Include("WorkHours").Where(x => x.Category.Name == Name);
                    var list = new List<Place>(places);
                    result.Places = list;

                }
                else if (City != "" && Name == "")
                {
                    var places = db.Places.Include("City").Include("Category").Include("Photos").Include("PlaceServices").Include("WorkHours").Where(x => x.City.Name == City);
                    var list = new List<Place>(places);
                    result.Places = list;
                    
                    foreach (var item in result.Places)
                    {
                        ArrayOfPlaceCategories.Add(item.CategoryId);
                    }
                    cs= db.CategoryServices.Include("Service").Where(x => ArrayOfPlaceCategories.Contains(x.CategoryId)).ToList();
                    result.IsAllSame = false;
                }
                else
                {
                    var places = db.Places.Include("City").Include("Category").Include("Photos").Include("PlaceServices").Include("WorkHours").Where(x => x.City.Name == City&&x.Category.Name==Name);
                    var list = new List<Place>(places);
                    result.Places = list;
                }
            }

            if (result.Places!=null)
            {
               
                if (Name!="")
                {
                    cs = db.CategoryServices.Include("Service").Where(x => x.Category.Name == Name).ToList();
                }
                int counter = 0;

                foreach (var item in result.Places)
                {
                    int CurrentWeekDay = (int)DateTime.Now.DayOfWeek;
                    if (CurrentWeekDay==0)
                    {
                        CurrentWeekDay = 7;
                    }
                    TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
                    WorkHour TodayWorkTime = item.WorkHours.FirstOrDefault(x => x.WeekNo == CurrentWeekDay);
                    if (CurrentTime > TodayWorkTime.OpenHour && CurrentTime < TodayWorkTime.CloseHour)
                    {
                        result.IsOpenedNow.Add(1);
                        counter++;
                    }
                    else
                    {
                        result.IsOpenedNow.Add(0);
                        counter++;
                    }

                }

                result.Count= result.Places.Count();
                result.CategoryServices = cs;
                return View(result);

            }
            else
            {
                return new HttpNotFoundResult("Pleace specify correct search options");
            }
        }

        public ActionResult Details(int id)
        {
            VwPlaceDetails details = new VwPlaceDetails();
            details.Place = db.Places.Include("City").Include("Category").Include("Photos").Include("PlaceServices").Include("WorkHours").FirstOrDefault(x=>x.Id==id);
            details.Comments = db.Comments.Include("User").Include("Reactions").Where(c=>c.PlaceId==id).ToList();
            details.Reactions = db.Reactions.Include("Comment").Where(r => r.Comment.PlaceId == id).ToList();


            return View(details);
        }

        [HttpPost]
        [Auth]
        public JsonResult PublishComment(string Comment, int ratingvalue, int PlaceId, HttpPostedFileBase[] filess )
        
{
            if (Comment!=null&& ratingvalue!= 0&&Comment!=""&&Comment.Length>140)
            {

                Comment toWrite = new Comment();
                toWrite.Rating = (byte)ratingvalue;
                toWrite.Text = Comment;
                toWrite.PlaceId = PlaceId;
                toWrite.UserId = Convert.ToInt32(Session["User"]);
                toWrite.Date = DateTime.Now;
                db.Comments.Add(toWrite);
                db.SaveChanges();
                foreach (HttpPostedFileBase foto in filess)
                {
                    if (foto.ContentLength > 0)
                    {
                        string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + foto.FileName;
                        string path = Path.Combine(Server.MapPath("~/Public/images/upload"), filename);
                        foto.SaveAs(path);
                        CommentPhoto ph= new CommentPhoto()
                        {
                           CommentId=toWrite.Id,
                           Photo=filename,
                        };
                        db.CommentPhotos.Add(ph);
                        db.SaveChanges();
                    }

                }

                return Json(new
                {

                    status = 200,
                    comment = Comment,
                    message = "ok",
                    url = "/place/details/" + PlaceId

                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = 404,
                message = "fuck"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Auth]
        public JsonResult Hcomment(int CommentId, bool IsAdd)
        {
            
            if (CommentId != 0)
            {
                 Reaction reaction= db.Reactions.Where(x => x.CommentId == CommentId).FirstOrDefault();
                if (reaction==null)
                {
                    Reaction brandnew = new Reaction();
                    brandnew.CommentId = CommentId;
                    brandnew.UserId = (int)Session["User"];
                    if (IsAdd)
                    {
                        brandnew.Type = 1;
                    }
                    else
                    {
                        brandnew.Type = 0;
                    }

                    db.Reactions.Add(brandnew);
                    db.SaveChanges();
                }
                else
                {
                    if (IsAdd)
                    {
                        reaction.Type = 1;
                    }
                    else
                    {
                        reaction.Type = 0;
                    }


                    db.SaveChanges();

                    
                }
                return Json(new

                 
                {
                    message="ok",
                    status=200,
                }, JsonRequestBehavior.AllowGet);

            }

            return Json(new
            {
                message="Xiyarxiyar sehvler cixir",
                status=404
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Auth]
        public JsonResult Uhcomment(int CommentId, bool IsAdd)
        {

            if (CommentId != 0)
            {
                Reaction reaction = db.Reactions.Where(x => x.CommentId == CommentId).FirstOrDefault();
                if (reaction == null)
                {
                    Reaction brandnew = new Reaction();
                    brandnew.CommentId = CommentId;
                    brandnew.UserId = (int)Session["User"];
                    if (IsAdd)
                    {
                        brandnew.Type = 2;
                    }
                    else
                    {
                        brandnew.Type = 0;
                    }

                    db.Reactions.Add(brandnew);
                    db.SaveChanges();
                }
                else
                {
                    if (IsAdd)
                    {
                        reaction.Type = 2;
                    }
                    else
                    {
                        reaction.Type = 0;
                    }


                    db.SaveChanges();


                }
                return Json(new


                {
                    message = "ok",
                    status = 200,
                }, JsonRequestBehavior.AllowGet);

            }

            return Json(new
            {
                message = "Xiyarxiyar sehvler cixir",
                status = 404
            }, JsonRequestBehavior.AllowGet);
        }
    }

    
}