using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class WorkHour
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int WeekNo { get; set; }

        public TimeSpan OpenHour { get; set; }

        public TimeSpan CloseHour { get; set; }

        public Place Place { get; set; }
    }
}