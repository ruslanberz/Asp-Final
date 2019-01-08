using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ASP_Final.Models;

namespace ASP_Final.DAL
{
    public class AspFinalContext : DbContext
    {
        public AspFinalContext() : base("AspFinalContext")

        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get;set;}
        public DbSet<CategoryService> CategoryServices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentPhoto> CommentPhotos { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceService> PlaceServices { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}