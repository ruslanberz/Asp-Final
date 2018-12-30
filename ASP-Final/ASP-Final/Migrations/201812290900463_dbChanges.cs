namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "Youtube", c => c.String());
            AddColumn("dbo.Places", "Facebook", c => c.String());
            AddColumn("dbo.Places", "Twitter", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "Twitter");
            DropColumn("dbo.Places", "Facebook");
            DropColumn("dbo.Places", "Youtube");
        }
    }
}
