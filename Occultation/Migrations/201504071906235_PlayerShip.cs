namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerShip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayerShips",
                c => new
                    {
                        PlayerShipId = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        XCoords = c.Int(nullable: false),
                        YCoords = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerShipId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlayerShips");
        }
    }
}
