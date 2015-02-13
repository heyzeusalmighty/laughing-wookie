using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DAL;
using Occultation.ViewModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class PlayerVMTests
    {


        [TestMethod]
        public void VM_ScienceTrack_StarBaseIsThere()
        {
            var repo = new FakeGameRepo();
            var vm = new PlayerViewModel(0, "thomas", repo);

            var starBaseExists = false;
            foreach (var tile in vm.ScienceTrack.StarTiles)
            {
                if (tile.Name != null)
                {
                    if (tile.Name.ToLower().Equals("star base", StringComparison.CurrentCultureIgnoreCase))
                    {
                        starBaseExists = true;
                    }
                }
                
            }

            Assert.IsTrue(starBaseExists);
        }

        [TestMethod]
        public void VM_ScienceTrack_StarBaseIsFirst()
        {
            var repo = new FakeGameRepo();
            var vm = new PlayerViewModel(0, "thomas", repo);

            
            var starBase = vm.ScienceTrack.StarTiles[0];
            var starBaseExists = starBase.Name.ToLower().Equals("star base");

            Assert.IsTrue(starBaseExists);
        }



    }
}
