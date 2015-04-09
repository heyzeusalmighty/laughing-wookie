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

        }
    }
}
