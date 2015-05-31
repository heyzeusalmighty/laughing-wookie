using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;
using Occultation.ViewModels;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class TurnTests
    {

        public static string GameIdentifier;
        public static int GameId;

        
        


        [TestMethod]
        public void Explore()
        {
            SetUpGame();
            using (var db = new GameModel())
            {
                var testGame = db.Games.FirstOrDefault(x => x.GameIdentifier == GameIdentifier);

                if (testGame != null)
                {
                    var firstPlayer = db.Players.First(x => x.GameId == testGame.GameId);

                    var turnTaker = new TakingTurns();
                    var tile = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier, 5, 4);
                    Assert.AreEqual(firstPlayer.DiscColor, tile.Occupied);

                    //Tests that the coordinates are already used
                    var unTile = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier,  5, 4);
                    Assert.IsNull(unTile);

                    //mark remaining Division One tiles as discovered
                    db.Database.ExecuteSqlCommand("update Mapdeck set XCoords = 1, YCoords = 1 where GameId = {0}",
                        GameId);

                    var noMoreTiles = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier, 5, 4);
                    Assert.IsNull(noMoreTiles);


                }
            }
            CleanUpYourMess();
        }

        [TestMethod]
        public void GetNeighbors_EvenColumns()
        {
            var model = new TakingTurns();
            var neighbors = model.GetNeighbors(2, 1);

            Assert.AreEqual(6, neighbors.Count());

            Assert.AreEqual(2, neighbors[0].X);
            Assert.AreEqual(0, neighbors[0].Y);

            Assert.AreEqual(3, neighbors[1].X);
            Assert.AreEqual(0, neighbors[1].Y);

            Assert.AreEqual(3, neighbors[2].X);
            Assert.AreEqual(1, neighbors[2].Y);

            Assert.AreEqual(2, neighbors[3].X);
            Assert.AreEqual(2, neighbors[3].Y);
            
            Assert.AreEqual(1, neighbors[4].X);
            Assert.AreEqual(1, neighbors[4].Y);

            Assert.AreEqual(1, neighbors[5].X);
            Assert.AreEqual(0, neighbors[5].Y);
        }

        [TestMethod]
        public void GetNeighbors_OddColumns()
        {
            var model = new TakingTurns();
            var neighbors = model.GetNeighbors(1, 1);

            Assert.AreEqual(6, neighbors.Count());

            Assert.AreEqual(1, neighbors[0].X);
            Assert.AreEqual(0, neighbors[0].Y);

            Assert.AreEqual(2, neighbors[1].X);
            Assert.AreEqual(1, neighbors[1].Y);

            Assert.AreEqual(2, neighbors[2].X);
            Assert.AreEqual(2, neighbors[2].Y);

            Assert.AreEqual(1, neighbors[3].X);
            Assert.AreEqual(2, neighbors[3].Y);

            Assert.AreEqual(0, neighbors[4].X);
            Assert.AreEqual(2, neighbors[4].Y);

            Assert.AreEqual(0, neighbors[5].X);
            Assert.AreEqual(1, neighbors[5].Y);
        }

        [TestMethod]
        public void GetNeighbors_EvenWildCards()
        {
            var model = new TakingTurns();
            var neighbors = model.GetNeighbors(2, 2);

            Assert.AreEqual(6, neighbors.Count());

            Assert.AreEqual(2, neighbors[0].X);
            Assert.AreEqual(1, neighbors[0].Y);

            Assert.AreEqual(3, neighbors[1].X);
            Assert.AreEqual(1, neighbors[1].Y);

            Assert.AreEqual(3, neighbors[2].X);
            Assert.AreEqual(2, neighbors[2].Y);

            Assert.AreEqual(2, neighbors[3].X);
            Assert.AreEqual(3, neighbors[3].Y);

            Assert.AreEqual(1, neighbors[4].X);
            Assert.AreEqual(2, neighbors[4].Y);

            Assert.AreEqual(1, neighbors[5].X);
            Assert.AreEqual(1, neighbors[5].Y);
        }

        [TestMethod]
        public void GetNeighbors_OddWildCards()
        {
            var model = new TakingTurns();
            var neighbors = model.GetNeighbors(9, 4);

            Assert.AreEqual(6, neighbors.Count());

            Assert.AreEqual(9, neighbors[0].X);
            Assert.AreEqual(3, neighbors[0].Y);

            Assert.AreEqual(10, neighbors[1].X);
            Assert.AreEqual(4, neighbors[1].Y);

            Assert.AreEqual(10, neighbors[2].X);
            Assert.AreEqual(5, neighbors[2].Y);

            Assert.AreEqual(9, neighbors[3].X);
            Assert.AreEqual(5, neighbors[3].Y);

            Assert.AreEqual(8, neighbors[4].X);
            Assert.AreEqual(5, neighbors[4].Y);

            Assert.AreEqual(8, neighbors[5].X);
            Assert.AreEqual(4, neighbors[5].Y);
        }
        
        [TestMethod]
        public void WormholeSetter()
        {
            var repo = new TurnRepository();

            var worm = new int[] {0, 1, 0, 1, 1, 1};

            const int first = 3;
            var firstHoles = repo.SetWormHoles(first, worm);

            Assert.AreEqual(1, firstHoles[0]);
            Assert.AreEqual(1, firstHoles[1]);
            Assert.AreEqual(1, firstHoles[2]);
            Assert.AreEqual(0, firstHoles[3]);
            Assert.AreEqual(1, firstHoles[4]);
            Assert.AreEqual(0, firstHoles[5]);




        }

        [TestMethod]
        public void CheckDiv()
        {
            var turn = new TakingTurns();

            Assert.AreEqual(2, turn.GetDivisionForCoordinates(8, 5));
            Assert.AreEqual(3, turn.GetDivisionForCoordinates(8, 7));
            Assert.AreEqual(1, turn.GetDivisionForCoordinates(7, 4));

            Assert.AreEqual(2, turn.GetDivisionForCoordinates(4, 5));
            Assert.AreEqual(3, turn.GetDivisionForCoordinates(2, 7));
            Assert.AreEqual(1, turn.GetDivisionForCoordinates(6, 6));
            Assert.AreEqual(2, turn.GetDivisionForCoordinates(5, 6));
            Assert.AreEqual(1, turn.GetDivisionForCoordinates(6, 4));


        }

        private void SetUpGame()
        {
            var repo = new EFGameRepo();
            var gameCreator = repo.CreateGame("For Testing");
            var game = repo.GetGame(gameCreator);

            GameIdentifier = game.GameIdentifier;
            GameId = game.GameId;

            var creatorModel = new GameCreator(repo);
            creatorModel.AddPlayerToGame(1, game.GameIdentifier);
            creatorModel.AddPlayerToGame(2, game.GameIdentifier);
            creatorModel.AddPlayerToGame(3, game.GameIdentifier);
            creatorModel.AddPlayerToGame(4, game.GameIdentifier);
            creatorModel.AddPlayerToGame(5, game.GameIdentifier);
            creatorModel.AddPlayerToGame(6, game.GameIdentifier);


            var model = new StartGameJob(repo);
            model.StartGame(game.GameIdentifier);
        }

        private void CleanUpYourMess()
        {
            using (var db = new GameModel())
            {
                var testGame = db.Games.FirstOrDefault(x => x.GameIdentifier == GameIdentifier);
                if (testGame != null)
                {
                    var gameId = testGame.GameId;
                    db.Database.ExecuteSqlCommand("delete from ShipModelComponents where ShipId in (select ShipId from PlayerShips where PlayerId in (select PlayerId from PLayer where GameId = {0}))", gameId);
                    db.Database.ExecuteSqlCommand(
                        "delete from PlayerShips where PlayerId in (select PlayerId from PLayer where GameId = {0})", gameId);
                    db.Database.ExecuteSqlCommand(
                        "delete from PlayerTrack where PlayerId in (select PlayerId from PLayer where GameId = {0})",
                        gameId);
                    db.Database.ExecuteSqlCommand(
                        "delete from PlayerShipModel where PlayerId in (select PlayerId from PLayer where GameId = {0})",
                        gameId);
                    db.Database.ExecuteSqlCommand("delete from PLayer where GameId = {0}", gameId);
                    db.Database.ExecuteSqlCommand("delete from Game where GameId = {0}", gameId);
                    db.Database.ExecuteSqlCommand("delete from GameDiscoveries where GameId = {0}", gameId);
                }
            }
        }
    }
}
