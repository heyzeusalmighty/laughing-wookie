namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscoveryTiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameDiscoveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscoveryId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Revealed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameDiscoveries");
        }
    }
}
