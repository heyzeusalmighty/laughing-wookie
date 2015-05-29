namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreDiscoveryTiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameDiscoveries", "GameId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameDiscoveries", "GameId");
        }
    }
}
