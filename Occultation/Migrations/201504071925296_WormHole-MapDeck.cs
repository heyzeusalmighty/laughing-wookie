namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WormHoleMapDeck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapDeck", "WormHoleIndex", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapDeck", "WormHoleIndex");
        }
    }
}
