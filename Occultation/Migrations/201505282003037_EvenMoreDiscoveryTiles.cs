namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvenMoreDiscoveryTiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameDiscoveries", "PlayerId", c => c.Int(nullable: false));
            AddColumn("dbo.GameDiscoveries", "Claimed", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameDiscoveries", "Claimed");
            DropColumn("dbo.GameDiscoveries", "PlayerId");
        }
    }
}
