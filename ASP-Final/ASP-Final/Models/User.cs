using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public List<Place> Places { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Comment> Comments { get; set; }
    }
}