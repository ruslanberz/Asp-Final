﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class VwHomeIndex
    {
        public List<Category> Categories { get; set; }
        public List<Place> Places { get; set; }
        public List<City> Cities { get; set; }
        
    }
}