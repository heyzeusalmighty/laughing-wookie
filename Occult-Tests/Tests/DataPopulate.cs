using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;
using Occultation.ViewModels;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class DataPopulate
    {

        private EFGameRepo db;
        private List<GameUser> Users;
            
            
        [TestMethod]
        public void CreateNewGame()
        {
            CleanUpDatabase();
            
            db = new EFGameRepo();
            Users = db.GetAllGameUsers();

            BuildGame(2);
            BuildGame(3);
            BuildGame(4);
            BuildGame(5);
            BuildGame(6);
        }


        public void BuildGame(int playerCount)
        {
            string title = "Dummy Name";
            switch (playerCount)
            {
                case 2:
                    title = "Two Players";
                    break;
                case 3:
                    title = "Three Players";
                    break;
                case 4:
                    title = "Four Players";
                    break;
                case 5:
                    title = "Five Players";
                    break;
                case 6:
                    title = "Six Players";
                    break;
                default:
                    title = "Dummy Name";
                    break;
            }







            var gameId = db.CreateGame(title);
            var game = db.GetGame(gameId);
            var creator = new GameCreator(db);

            foreach (var user in Users.Take(playerCount))
            {
                creator.AddPlayerToGame(user.UserId, game.GameIdentifier);
            }

            var start = new StartGameJob(db);
            start.StartGame(game.GameIdentifier);
            
        }

        public void CleanUpDatabase()
        {

            using (var context = new GameModel())
            {
                //insert into GameUsers (UserName, EmailAddress) values ('GroverCleveCleve', 'x@x.com');
                //insert into GameUsers (UserName, EmailAddress) values ('WillieTaft', 'x@x.com');

                //GameUsers
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Game]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [MapDeck]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Player]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerShipModel]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerTrack]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ScienceTrack]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ShipModelComponents]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Tiles]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerShips]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [GameDiscoveries]");
                //context.Database.ExecuteSqlCommand("TRUNCATE TABLE []");
            }


        }

        public void CreateIdentities()
        {

        }
    }
}
