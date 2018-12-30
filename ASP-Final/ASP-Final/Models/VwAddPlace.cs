using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Final.Models;

namespace ASP_Final.Models
{
    public class VwAddPlace
    {
        public List<Category> Categories { get; set; }
        public List<City> Cities { get; set; }
       public Place place { get; set; }
        public List<int> ser { get; set; }
        public List<TimeSpan> Times { get; set; }
        public string lat { get; set; }
    }
}