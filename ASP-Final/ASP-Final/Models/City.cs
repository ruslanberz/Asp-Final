using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }


        public List<Place> Places { get; set; }

    }
}