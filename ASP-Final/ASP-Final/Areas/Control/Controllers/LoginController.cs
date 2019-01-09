using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.DAL;
using System.Web.Helpers;

namespace ASP_Final.Areas.Control.Controllers
{
    public class LoginController : Controller
    {

        private readonly AspFinalContext db = new AspFinalContext();
        // GET: Control/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Admin admin)
        {

            if (admin!=null)
            {
                Admin adm = db.Admins.Where(x => x.Email == admin.Email).FirstOrDefault();


                if (adm!=null)
                {
                    if (Crypto.VerifyHashedPassword(adm.Password, admin.Password))
                    {
                        Session["Admin"] = adm.Id;
                        return RedirectToAction("index", "dashboard");
                    }
                }
            }
            return Redirect("/control/login/err");
        }

        public ActionResult Err()


        {
            return View();

        }

        public ActionResult Logout()


        {
            Session["Admin"] = null;
            return Redirect("/control/login/index");
        }

    }
}