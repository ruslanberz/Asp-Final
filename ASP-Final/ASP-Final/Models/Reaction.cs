using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class Reaction
    {
        
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public byte Type { get; set; }

        public virtual User User { get; set; }
        public virtual Comment Comment { get; set; }
    }
}