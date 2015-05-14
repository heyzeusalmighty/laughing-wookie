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
                    var tile = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier, 1, 5, 4);
                    Assert.AreEqual(firstPlayer.DiscColor, tile.Occupied);

                    //Tests that the coordinates are already used
                    var unTile = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier, 1, 5, 4);
                    Assert.IsNull(unTile);

                    //mark remaining Division One tiles as discovered
                    db.Database.ExecuteSqlCommand("update Mapdeck set XCoords = 1, YCoords = 1 where GameId = {0}",
                        GameId);

                    var noMoreTiles = turnTaker.Explore(firstPlayer.Username, testGame.GameIdentifier, 1, 5, 4);
                    Assert.IsNull(noMoreTiles);


                }
            }
            CleanUpYourMess();
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
                }
            }
        }
    }
}
