using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Final.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        [Required]
        public byte Rating { get; set; }
        [Required]
        [MinLength(140)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Place Place { get; set; }
        public virtual List<Reaction> Reactions { get; set; }
        public virtual List<CommentPhoto> CommentPhotos { get; set; }
    }
}