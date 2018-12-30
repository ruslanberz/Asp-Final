using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class Service
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<CategoryService> CategoryServices { get; set; }

        public List<PlaceService> PlaceServices { get; set; }
    }
}