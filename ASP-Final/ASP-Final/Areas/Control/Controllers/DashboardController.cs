using ASP_Final.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_Final.Areas.Control.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Control/Dashboard
        [AdminAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}