using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Occultation.DAL;
using Occultation.ViewModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class GameCreatorTests
    {
        [TestMethod]
        public void GameCreator_AddPlayerToGame_GameNotFound()
        {
            var repo = new FakeGameRepo();
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(1, "fakeGuid");

            Assert.AreEqual("Game Not Found", msg);
        }

        [TestMethod]
        public void GameCreator_AddPlayerToGame_DoublePlayer()
        {
            var repo = new FakeGameRepo();
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(1, "realGuid");

            Assert.AreEqual("Cannot play twice in one game", msg);
        }

        [TestMethod]
        public void GameCreator_AddPlayerToGame_DoubleColor()
        {
            var repo = new FakeGameRepo();
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(6, "realGuid");

            Assert.AreEqual("success", msg);
        }

        [TestMethod]
        public void GameCreator_AddPlayerToGame_PlayerAdded()
        {

            var repo = new FakeGameRepo();
            Assert.IsNull(repo.AddedPlayers);
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(6, "realGuid");

            Assert.AreEqual(6, repo.AddedPlayers.Count);
        }

        [TestMethod]
        public void GameCreator_StartGame_CountScienceTracks()
        {
            var repo = new FakeGameRepo();
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(6, "realGuid");

            Assert.AreEqual(5, repo.Tracks.Count);
        }

        [TestMethod]
        public void GameCreator_StartGame_CountShips()
        {
            var repo = new FakeGameRepo();
            var model = new GameCreator(repo);
            var msg = model.AddPlayerToGame(6, "realGuid");

            model.StartGame("realGuid");

            //Assert.AreEqual(5, repo.ShipModel.Count);

            //Assert.AreEqual(15, repo.ShipComponents.Count);
        }
    
    }

}
