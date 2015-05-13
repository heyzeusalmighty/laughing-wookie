using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class TurnTests
    {
        [TestMethod]
        public void Explore()
        {
            using (var db = new GameModel())
            {
                const int gameId = 1002;
                const string userName = "AbeLincoln";
                var testGame = db.Games.FirstOrDefault(x => x.GameId == gameId);

                var turnTaker = new TakingTurns();
                var tile = turnTaker.Explore(userName, testGame.GameIdentifier, 1, 5, 4);

                Assert.AreEqual("White", tile.Occupied);
            }
        }
    }
}
