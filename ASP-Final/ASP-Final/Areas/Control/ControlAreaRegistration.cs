﻿using System.Web.Mvc;

namespace ASP_Final.Areas.Control
{
    public class ControlAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Control";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Control_default",
                "Control/{controller}/{action}/{id}",
                new {controller="login", action = "Index", id = UrlParameter.Optional },
                new[] { "ASP_Final.Areas.Control.Controllers" }
            );
        }
    }
}