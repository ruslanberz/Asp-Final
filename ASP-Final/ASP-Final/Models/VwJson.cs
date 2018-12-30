using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class VwJson
    {
        public Place place { get; set; }
        public List<int> services { get; set; }
        public List<WorkHour> Workhours { get; set; }
    }
}