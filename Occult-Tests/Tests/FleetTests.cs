using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DataModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void Create_CreateFleet()
        {
            var fleet = new Fleet(InterceptorsForEveryone());
            Assert.AreEqual(3, fleet.ActiveShips.Count);
        }

        [TestMethod]
        public void AddShip_AddCruiserToFleet()
        {
            var fleet = new Fleet(InterceptorsForEveryone());
            var cruiser = new Cruiser();
            fleet.AddShip(cruiser);

            Assert.AreEqual(4, fleet.ActiveShips.Count);

           
        }



        private List<Ship> InterceptorsForEveryone()
        {
            return new List<Ship>
            {
                new Interceptor(), new Interceptor(), new Interceptor()
            };
        }
    }
}
