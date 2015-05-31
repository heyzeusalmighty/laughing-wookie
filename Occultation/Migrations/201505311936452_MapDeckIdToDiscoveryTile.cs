namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapDeckIdToDiscoveryTile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameDiscoveries", "MapDeckId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameDiscoveries", "MapDeckId");
        }
    }
}
