using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class VwHomeZibilleri
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slogan { get; set; }
        public string PhotoName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public int CommentCount { get; set; }
        public double Rating { get; set; }
        public List<WorkHour> WorkHours { get; set; }
    }
}