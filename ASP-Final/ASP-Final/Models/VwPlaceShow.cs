using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class VwPlaceShow
    {
        public List<Place> Places { get; set; }
        public List<CategoryService> CategoryServices { get; set; }
        public int Count { get; set; }
    }
}