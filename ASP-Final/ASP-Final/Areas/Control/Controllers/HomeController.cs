using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.DAL;
using ASP_Final.Models;
using System.Web.Helpers;

namespace ASP_Final.Areas.Control.Controllers
{
    public class HomeController : Controller
    {
        AspFinalContext db = new AspFinalContext();
        // GET: Control/Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Dashboard(string email, string password)

        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new
                {
                    message = "Please enter email",
                    status = 401

                });
            }

            if (string.IsNullOrEmpty(password))
            {
                return Json(new
                {
                    message = "Please enter valid email/password",
                    status = 401

                });

            }

            Admin adm = db.Admins.Where(x => x.Email == email).FirstOrDefault();
            if (adm==null)
            {
                return Json(new
                {
                    message = "Please enter valid email/password",
                    status = 401

                });

            }
            else
            {
                if (!Crypto.VerifyHashedPassword(adm.Password, password))
                {
                    return Json(new
                    {
                        message = "Please enter valid email/password",
                        status = 401

                    });
                }
                else
                {
                    return Json(new
                    {
                        message = "Logged in successfully",
                        url = "/home/dashboard",
                        status = 200

                    });
                }
               
            }
           
        }
    }
}