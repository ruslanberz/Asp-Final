namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesAddUSerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Places", "User_Id", "dbo.Users");
            DropIndex("dbo.Places", new[] { "User_Id" });
            RenameColumn(table: "dbo.Places", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Places", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Places", "UserId");
            AddForeignKey("dbo.Places", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Places", "UserId", "dbo.Users");
            DropIndex("dbo.Places", new[] { "UserId" });
            AlterColumn("dbo.Places", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Places", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Places", "User_Id");
            AddForeignKey("dbo.Places", "User_Id", "dbo.Users", "Id");
        }
    }
}
