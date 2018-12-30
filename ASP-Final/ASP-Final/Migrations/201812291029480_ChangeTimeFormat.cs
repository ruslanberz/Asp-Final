namespace ASP_Final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTimeFormat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkHours", "OpenHour", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.WorkHours", "CloseHour", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkHours", "CloseHour", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkHours", "OpenHour", c => c.DateTime(nullable: false));
        }
    }
}
