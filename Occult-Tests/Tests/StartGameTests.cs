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
    public class StartGameTests
    {


        [TestMethod]
        public void StartGame_PlayerCount()
        {
            var repo = new FakeGameRepo();
            var model = new StartGameJob(repo);
            model.StartGame("realGuid");

            foreach (var player in model.Players)
            {
                Console.WriteLine(player.DiscColor);
            }

            var playerSet = true;
            foreach (var deck in model.PlayerDeck)
            {
                Console.WriteLine(deck.XCoords + " :: " + deck.YCoords);
                if (deck.PlayerId == null)
                {
                    playerSet = false;
                }

            }

            Assert.IsTrue(playerSet);
        }

        [TestMethod]
        public void StartGame_ShipCount()
        {
            var repo = new FakeGameRepo();
            var model = new StartGameJob(repo);
            model.StartGame("realGuid");

            var playerCount = model.Players.Count() * 2;
            var ships = repo.PlayerShips.Count();

            Assert.AreEqual(playerCount, ships);
        }
    }
}
