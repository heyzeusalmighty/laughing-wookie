namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingUpTurnSystem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "PassOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "WhosTurn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Player", "WhosTurn");
            DropColumn("dbo.Player", "PassOrder");
        }
    }
}
