using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL;
using Occultation.ViewModels;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class DataPopulate
    {
        

        [TestMethod]
        public void CreateNewGame()
        {
            var db = new EFGameRepo();
            var users = db.GetAllGameUsers();

            var gameId = db.CreateGame();
            var game = db.GetGame(gameId);



            var creator = new GameCreator(db);
            foreach (var user in users)
            {
                creator.AddPlayerToGame(user.UserId, game.GameIdentifier, "");
            }

            creator.StartGame(game.GameIdentifier);
        }
    }
}
