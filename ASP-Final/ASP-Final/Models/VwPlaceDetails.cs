using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class VwPlaceDetails
    {
        public Place Place  { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}