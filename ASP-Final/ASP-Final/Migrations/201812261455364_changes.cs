namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "Keyword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "Keyword");
        }
    }
}
