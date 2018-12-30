using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class Photo
    {

        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string PhotoName { get; set; }

        public Place Place { get; set; }
    }
}