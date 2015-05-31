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

            var playerCount = model.Players.Count()*2;
            var ships = repo.PlayerShips.Count();

            Assert.AreEqual(playerCount, ships);
        }

        [TestMethod]
        public void CreateAGameForReal()
        {
            var clean = new DataPopulate();
            clean.CleanUpDatabase();

            

            var repo = new EFGameRepo();
            var gameCreator = repo.CreateGame("For Testing");
            var game = repo.GetGame(gameCreator);

            var creatorModel = new GameCreator(repo);
            creatorModel.AddPlayerToGame(1, game.GameIdentifier);
            creatorModel.AddPlayerToGame(2, game.GameIdentifier);
            creatorModel.AddPlayerToGame(3, game.GameIdentifier);
            creatorModel.AddPlayerToGame(4, game.GameIdentifier);
            creatorModel.AddPlayerToGame(5, game.GameIdentifier);
            creatorModel.AddPlayerToGame(6, game.GameIdentifier);

            //if there are 6 players, it starts the game automagically
            var model = new StartGameJob(repo);
            model.StartGame(game.GameIdentifier);



           
        }

        [TestMethod]
        public void DiscoveryTiles_Shuffle()
        {
            var repo = new FakeGameRepo();
            var model = new StartGameJob(repo);
            const int numberOfTheBeast = 666;
            model.BuildDiscoveryTiles(numberOfTheBeast);

            Assert.AreNotEqual(model.DiscoveryTiles[0].Id, 1);

            foreach (var tile in repo.Discoveries)
            {
                Assert.AreEqual(numberOfTheBeast, tile.GameId);
            }
        }
}
}
