using Occultation.DAL.EF;

namespace Occultation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Occultation.DAL.EF.GameModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Occultation.DAL.EF.GameModel";
        }

        protected override void Seed(Occultation.DAL.EF.GameModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.GameUsers.AddOrUpdate(
                
                p => p.UserName,
                new GameUser { UserName = "GeorgieWashington", EmailAddress = "x@x.com"},
                new GameUser { UserName = "GroverCleveCleve", EmailAddress = "x@x.com"},
                new GameUser { UserName = "WillieTaft", EmailAddress = "x@x.com"},
                new GameUser { UserName = "AbeLincoln", EmailAddress = "x@x.com"},
                new GameUser { UserName = "TeddyRoooooo", EmailAddress = "x@x.com"},
                new GameUser { UserName = "TommyJefferson", EmailAddress = "x@x.com"}
                );

        }
    }
}
