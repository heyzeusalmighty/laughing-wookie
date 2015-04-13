namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPlayerIdToMapDeck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapDeck", "PlayerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapDeck", "PlayerId");
        }
    }
}
