namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableXAndYCoords : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlayerShips", "XCoords", c => c.Int());
            AlterColumn("dbo.PlayerShips", "YCoords", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlayerShips", "YCoords", c => c.Int(nullable: false));
            AlterColumn("dbo.PlayerShips", "XCoords", c => c.Int(nullable: false));
        }
    }
}
