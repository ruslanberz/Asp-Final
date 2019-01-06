using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class PlaceService
    {

        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int ServiceId { get; set; }

        public Place Place { get; set; }
        public virtual Service Service { get; set; }
    }
}