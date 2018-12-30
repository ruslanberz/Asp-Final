using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class CategoryService
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ServiceId { get; set; }

        public Category Category { get; set; }
        public Service Service { get; set; }
    }
}