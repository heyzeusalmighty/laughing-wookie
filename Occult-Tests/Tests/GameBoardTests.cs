using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL;
using Occultation.DataModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class GameBoardTests
    {

       

        [TestMethod]
        public void GameBoard_StartFourPlayerTheGame()
        {
            var fourPlayerGame = new GameBoard(SetUpFourPlayers());

            Assert.AreEqual(4, fourPlayerGame.Users.Count);

            Assert.AreEqual(8, fourPlayerGame.DivisionOne.Count);

        }

        [TestMethod]
        public void GameBoard_TurnOrder()
        {
            var fourPlayerGame = new GameBoard(SetUpFourPlayers());

            var counter = 0;
            var players = fourPlayerGame.Users.OrderBy(x => x.TurnOrder);
            foreach (var player in players)
            {
                Assert.AreEqual(counter, player.TurnOrder);
                counter++;
            }


        }

        [TestMethod]
        public void GameBoard_NewGameNoPasses()
        {
            var fourPlayerGame = new GameBoard(SetUpFourPlayers());

            var noPass = fourPlayerGame.AllUsersHavePassed();
            Assert.IsFalse(noPass);
        }

        [TestMethod]
        public void GameBoard_TakeTurns()
        {
            var fourPlayerGame = new GameBoard(SetUpFourPlayers());

            var noPass = fourPlayerGame.AllUsersHavePassed();
            Assert.IsFalse(noPass);
            fourPlayerGame.Turns();
            Assert.IsTrue(fourPlayerGame.AllUsersHavePassed());
        }

        [TestMethod]
        public void GameBoard_GetBoardFromRepo()
        {
            var repo = new FakeGameRepo();
            var game = repo.GetGameBoard(1);
            Assert.IsFalse(game.AllUsersHavePassed());
        }

        [TestMethod]
        public void GameBoard_GetNullBoardFromRepo()
        {
            var repo = new FakeGameRepo();
            var game = repo.GetGameBoard(1000);
            Assert.IsNull(game);
        }


        public List<User> SetUpFourPlayers()
        {
            return new List<User>
            {
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Black, Pass = false},
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Red, Pass = false },
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Blue, Pass = false },
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Green, Pass = false },
            };
        }
    }
}
