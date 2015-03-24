namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DumbDumb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "Title");
        }
    }
}
