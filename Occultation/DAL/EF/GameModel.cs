using Occultation.DataModels;

namespace Occultation.DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GameModel : DbContext
    {
        public GameModel()
            : base("name=GameModel")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameUser> GameUsers { get; set; }
        public virtual DbSet<MapDeck> MapDecks { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerShipModel> PlayerShipModels { get; set; }
        public virtual DbSet<PlayerTrack> PlayerTracks { get; set; }
        public virtual DbSet<ScienceTrack> ScienceTracks { get; set; }
        public virtual DbSet<ShipModelComponent> ShipModelComponents { get; set; }
        public virtual DbSet<Tile> Tiles { get; set; }

        public virtual DbSet<EmailSettings> EmailSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.GameIdentifier)
                .IsUnicode(false);

            //modelBuilder.Entity<Game>()
            //    .Property(e => e.Title)
            //    .IsUnicode(false);

            modelBuilder.Entity<GameUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<GameUser>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<MapDeck>()
                .Property(e => e.Occupied)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.DiscColor)
                .IsUnicode(false);

            modelBuilder.Entity<PlayerShipModel>()
                .Property(e => e.ModelName)
                .IsUnicode(false);

            modelBuilder.Entity<PlayerTrack>()
                .Property(e => e.Track)
                .IsUnicode(false);

            modelBuilder.Entity<Tile>()
                .Property(e => e.ControlledBy)
                .IsUnicode(false);
        }
    }
}
