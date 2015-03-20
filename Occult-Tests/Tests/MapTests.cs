using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DataModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class MapTests
    {

        [TestMethod]
        public void GetDiv3_VictoryPoints()
        {
            var tiles = new AvailableMapTile();
            var div3 = tiles.Div3Tiles();

            Assert.IsTrue(div3.All(x => x.VictoryPoints != null));
        }

        [TestMethod]
        public void GetDiv3_MapId()
        {
            var tiles = new AvailableMapTile();
            var div3 = tiles.Div3Tiles();

            Assert.IsTrue(div3.All(x => x.MapId > 300));
        }

        [TestMethod]
        public void GetDiv3_Occupied()
        {
            var tiles = new AvailableMapTile();
            var div3 = tiles.Div3Tiles();

            Assert.IsTrue(div3.All(x => x.Occupied != null));
        }

        [TestMethod]
        public void GetDiv3_Reward()
        {
            var tiles = new AvailableMapTile();
            var div3 = tiles.Div3Tiles();
            Assert.IsTrue(div3.All(x => x.Reward != null));
        }

        [TestMethod]
        public void GetDiv3_WormHoles()
        {
            var tiles = new AvailableMapTile();
            var div = tiles.Div3Tiles();

            foreach (var tile in div)
            {
                Assert.AreEqual(6, tile.Wormholes.Count());
            }
        }

        [TestMethod]
        public void GetDiv2_VictoryPoints()
        {
            var tiles = new AvailableMapTile();
            var div2 = tiles.Div2Tiles();

            Assert.IsTrue(div2.All(x => x.VictoryPoints != null));
        }

        [TestMethod]
        public void GetDiv2_MapId()
        {
            var tiles = new AvailableMapTile();
            var div2 = tiles.Div2Tiles();

            Assert.IsTrue(div2.All(x => x.MapId > 200));
            Assert.IsTrue(div2.All(x => x.MapId < 300));
        }

        [TestMethod]
        public void GetDiv2_Occupied()
        {
            var tiles = new AvailableMapTile();
            var div2 = tiles.Div2Tiles();

            Assert.IsTrue(div2.All(x => x.Occupied != null));
        }

        [TestMethod]
        public void GetDiv2_Reward()
        {
            var tiles = new AvailableMapTile();
            var div2 = tiles.Div2Tiles();
            Assert.IsTrue(div2.All(x => x.Reward != null));
        }

        [TestMethod]
        public void GetDiv2_WormHoles()
        {
            var tiles = new AvailableMapTile();
            var div = tiles.Div2Tiles();

            foreach (var tile in div)
            {
                Assert.AreEqual(6, tile.Wormholes.Count());
            }
        }


        [TestMethod]
        public void GetDiv1_VictoryPoints()
        {
            var tiles = new AvailableMapTile();
            var div1 = tiles.Div1Tiles();

            Assert.IsTrue(div1.All(x => x.VictoryPoints != null));
        }

        [TestMethod]
        public void GetDiv1_MapId()
        {
            var tiles = new AvailableMapTile();
            var div1 = tiles.Div1Tiles();

            Assert.IsTrue(div1.All(x => x.MapId > 100));
            Assert.IsTrue(div1.All(x => x.MapId < 200));
        }

        [TestMethod]
        public void GetDiv1_Occupied()
        {
            var tiles = new AvailableMapTile();
            var div1 = tiles.Div1Tiles();

            Assert.IsTrue(div1.All(x => x.Occupied != null));
        }

        [TestMethod]
        public void GetDiv1_Reward()
        {
            var tiles = new AvailableMapTile();
            var div1 = tiles.Div1Tiles();
            Assert.IsTrue(div1.All(x => x.Reward != null));
        }

        [TestMethod]
        public void GetDiv1_WormHoles()
        {
            var tiles = new AvailableMapTile();
            var div = tiles.Div1Tiles();

            foreach (var tile in div)
            {
                Assert.AreEqual(6, tile.Wormholes.Count());
            }
        }

        [TestMethod]
        public void All_AliensWhereAliens()
        {
            var tiles = new AvailableMapTile();
            var div = tiles.Div1Tiles();
            div.AddRange(tiles.Div2Tiles());
            div.AddRange(tiles.Div3Tiles());

            foreach (var tile in div)
            {
                if (tile.Aliens > 0)
                {
                    Assert.IsTrue(tile.Occupied == "Aliens");
                }
            }
        }
    }
}
