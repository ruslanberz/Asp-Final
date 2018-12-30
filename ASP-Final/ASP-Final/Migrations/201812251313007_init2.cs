namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaceServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(),
                        Slogan = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Lat = c.String(),
                        Lng = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.CityId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Rating = c.Byte(nullable: false),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CommentPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Reactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        PhotoName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.WorkHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        WeekNo = c.Int(nullable: false),
                        OpenHour = c.DateTime(nullable: false),
                        CloseHour = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            DropColumn("dbo.Users", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Username", c => c.String(nullable: false));
            DropForeignKey("dbo.PlaceServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.WorkHours", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceServices", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Photos", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Reactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Places", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reactions", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.CommentPhotoes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Places", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Places", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategoryServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.CategoryServices", "CategoryId", "dbo.Categories");
            DropIndex("dbo.WorkHours", new[] { "PlaceId" });
            DropIndex("dbo.Photos", new[] { "PlaceId" });
            DropIndex("dbo.Reactions", new[] { "UserId" });
            DropIndex("dbo.Reactions", new[] { "CommentId" });
            DropIndex("dbo.CommentPhotoes", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "User_Id" });
            DropIndex("dbo.Places", new[] { "CityId" });
            DropIndex("dbo.Places", new[] { "CategoryId" });
            DropIndex("dbo.PlaceServices", new[] { "ServiceId" });
            DropIndex("dbo.PlaceServices", new[] { "PlaceId" });
            DropIndex("dbo.CategoryServices", new[] { "ServiceId" });
            DropIndex("dbo.CategoryServices", new[] { "CategoryId" });
            DropTable("dbo.WorkHours");
            DropTable("dbo.Photos");
            DropTable("dbo.Reactions");
            DropTable("dbo.CommentPhotoes");
            DropTable("dbo.Comments");
            DropTable("dbo.Cities");
            DropTable("dbo.Places");
            DropTable("dbo.PlaceServices");
            DropTable("dbo.Services");
            DropTable("dbo.CategoryServices");
            DropTable("dbo.Categories");
        }
    }
}
