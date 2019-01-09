using System;
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
        public List<VwHomeZibilleri> Zibiller { get; set; }
        public VwTopCities One { get; set; }
        public VwTopCities Two { get; set; }
        public VwTopCities Three { get; set; }
        public VwTopCities Four { get; set; }
        public List<Blog>  LatestBlogs { get; set; }
        public int AllPlaceCount { get; set; }

    }
}