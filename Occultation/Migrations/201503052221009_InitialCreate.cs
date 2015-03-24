namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                        CurrentPlayer = c.Int(),
                        GameIdentifier = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.GameUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50, unicode: false),
                        EmailAddress = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MapDeck",
                c => new
                    {
                        MapDeckId = c.Int(nullable: false, identity: true),
                        MapId = c.Int(nullable: false),
                        Division = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        Revealed = c.Boolean(nullable: false),
                        XCoords = c.Int(),
                        YCoords = c.Int(),
                        Occupied = c.String(maxLength: 25, unicode: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MapDeckId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        GameId = c.Int(nullable: false),
                        DiscColor = c.String(nullable: false, maxLength: 12, unicode: false),
                        CurrentOrange = c.Int(nullable: false),
                        CurrentBrown = c.Int(nullable: false),
                        CurrentPink = c.Int(nullable: false),
                        OrangeIncome = c.Int(nullable: false),
                        BrownIncome = c.Int(nullable: false),
                        PinkIncome = c.Int(nullable: false),
                        Pass = c.Boolean(nullable: false),
                        TurnOrder = c.Int(nullable: false),
                        TotalDiscs = c.Int(nullable: false),
                        AvailableDiscs = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AvailableColonyShips = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerShipModel",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 25, unicode: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModelId);
            
            CreateTable(
                "dbo.PlayerTrack",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        Track = c.String(nullable: false, maxLength: 25, unicode: false),
                        TileId = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackId);
            
            CreateTable(
                "dbo.ScienceTrack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        ScienceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShipModelComponents",
                c => new
                    {
                        ComId = c.Int(nullable: false, identity: true),
                        ComponentId = c.Int(),
                        ShipId = c.Int(),
                    })
                .PrimaryKey(t => t.ComId);
            
            CreateTable(
                "dbo.Tiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XCoords = c.Int(nullable: false),
                        YCoords = c.Int(nullable: false),
                        TileId = c.Int(nullable: false),
                        ControlledBy = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tiles");
            DropTable("dbo.ShipModelComponents");
            DropTable("dbo.ScienceTrack");
            DropTable("dbo.PlayerTrack");
            DropTable("dbo.PlayerShipModel");
            DropTable("dbo.Player");
            DropTable("dbo.MapDeck");
            DropTable("dbo.GameUsers");
            DropTable("dbo.Game");
        }
    }
}
