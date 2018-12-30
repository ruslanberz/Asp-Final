using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class CommentPhoto
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public string Photo { get; set; }

        public Comment Comment { get; set; }
    }
}