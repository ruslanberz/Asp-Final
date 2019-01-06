namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorieschange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.CommentPhotoes", "Photo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentPhotoes", "Photo", c => c.String());
            AlterColumn("dbo.Comments", "Text", c => c.String());
        }
    }
}
