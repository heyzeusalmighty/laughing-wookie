namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailSettingsAdjustment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailSettings", "AdminEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailSettings", "AdminEmail");
        }
    }
}
