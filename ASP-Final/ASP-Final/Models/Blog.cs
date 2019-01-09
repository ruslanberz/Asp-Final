using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Categ { get; set; }
        public string SmallImage { get; set; }
        public string BigImage { get; set; }
    }
}