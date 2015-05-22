namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShipComponentName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShipModelComponents", "ComponentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShipModelComponents", "ComponentName");
        }
    }
}
