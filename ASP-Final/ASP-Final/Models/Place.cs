using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Final.Models;
namespace ASP_Final.Models
{
    public class Place
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public int CategoryId { get; set; }
        public int CityId{ get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public City City { get; set; }
        public User User { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Youtube { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public List<Comment> Comments { get; set; }
        public List<WorkHour> WorkHours { get; set; }
        public List<Photo> Photos { get; set; }
        public string Keyword { get; set; }
        public virtual List<PlaceService> PlaceServices { get; set; }

}
}