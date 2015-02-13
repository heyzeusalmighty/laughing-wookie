using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DataModels;
using Assert = NUnit.Framework.Assert;

namespace Occult_Tests.Tests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void BuildInterceptor()
        {
            var intercept = new Interceptor();
            Assert.AreEqual(3, intercept.Components.Count());
        }

        [TestMethod]
        public void BuildCruiser()
        {
            var cruise = new Cruiser();
            Assert.AreEqual(5, cruise.Components.Count());
        }

        [TestMethod]
        public void BuildAncient()
        {
            var ancient = new Ancient();
            Assert.AreEqual(1, ancient.HullPoints);
        }

        [TestMethod]
        public void Upgrade_AddTooHighPowerCostComponent()
        {
            var inter = new Interceptor();
            var valid = inter.Upgrade(new AntiMatterCannon());

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void Upgrade_NoSlotsAvailable()
        {
            var inter = new Interceptor();
            var firstAdd = inter.Upgrade(new GaussShield());
            Assert.IsTrue(firstAdd);
            var secondAdd = inter.Upgrade(new ImprovedHull());
            Assert.IsFalse(secondAdd);
        }

        [TestMethod]
        public void Replace_UpgradePowerSystem()
        {
            var inter = new Interceptor();
            var nuclear = inter.Components.FirstOrDefault(x => x.Name == "Nuclear Source");
            var replace = inter.Replace(nuclear, new TachyonSource());
            Assert.IsTrue(replace);
            Assert.AreEqual(3, inter.Components.Count());
        }

        [TestMethod]
        public void Upgrade_ToComputer()
        {
            var inter = new Interceptor();
            var up = inter.Upgrade(new PositronComputer());
            Assert.IsTrue(up);
        }

        [TestMethod]
        public void Replace_ReplaceEqualComponent()
        {
            var inter = new Interceptor();
            var cannon = inter.Components.FirstOrDefault(x => x.Name == "Ion Cannon");
            var replace = inter.Replace(cannon, new IonCannon());
            Assert.IsTrue(replace);
            Assert.AreEqual(3, inter.Components.Count());
        }

        [TestMethod]
        public void Replace_ReplaceWithMissiles()
        {
            var inter = new Interceptor();
            var cannon = inter.Components.FirstOrDefault(x => x.Name == "Ion Cannon");
            var replace = inter.Replace(cannon, new PlasmaMissile());
            Assert.IsTrue(replace);
            Assert.AreEqual(3, inter.Components.Count());
        }

        [TestMethod]
        public void Create_HasDrive()
        {

            var inter = new Interceptor();
            Assert.AreEqual(1, inter.Drive);
        }

        [TestMethod]
        public void Replace_ReplaceDriveWithGun()
        {
            var inter = new Interceptor();
            var drive = inter.Components.FirstOrDefault(x => x.Name == "Nuclear Drive");
            var replace = inter.Replace(drive, new IonCannon());
            Assert.IsFalse(replace);
        }

        [TestMethod]
        public void Replace_ReplaceNuclearToHighestPowerEngine()
        {
            var inter = new Interceptor();
            var drive = inter.Components.FirstOrDefault(x => x.Name == "Nuclear Drive");
            var replace = inter.Replace(drive, new TachyonDrive());
            Assert.IsFalse(replace);
        }

        [TestMethod]
        public void Create_IsAtActiveStatus()
        {
            var cruise = new Cruiser();
            Assert.AreEqual(ShipStatus.Active, cruise.Status);
        }

        [TestMethod]
        public void DetermineHit_CruiserGetsHit()
        {
            var cruise = new Cruiser();
            var hit = cruise.DetermineHit(5, 2);
            Assert.IsTrue(hit);
        }

        [TestMethod]
        public void DetermineHit_CruiserJustMisses()
        {
            var cruise = new Cruiser();
            Console.WriteLine(cruise.Shields);
            var hit = cruise.DetermineHit(4, 1);
            Assert.IsFalse(hit);
        }

        [TestMethod]
        public void DetermineHit_ShieldedCruiserJustMisses()
        {
            var cruise = new Cruiser();
            var upgrade = cruise.Upgrade(new GaussShield());
            Assert.IsTrue(upgrade);
            Console.WriteLine(cruise.Shields);
            var hit = cruise.DetermineHit(4, 2);
            Assert.IsFalse(hit);
        }

        [TestMethod]
        public void Create_IdIsPopulated()
        {
            var cruise = new Cruiser();
            Console.WriteLine(cruise.ShipId);
            Assert.IsNotNull(cruise.ShipId);
        }

        [TestMethod]
        public void Create_TwoInterceptersMatch()
        {
            var inter = new Interceptor();
            var upgrade = inter.Upgrade(new GaussShield());
            Assert.IsTrue(upgrade);

            //How are you going to build the logic that allows every  new interceptor
            //to have that upgrade?  Should you build a model and then copy it for all
            //new ships?
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void Replace_TakeAwayYellowDice()
        {
            var missileBoat = new Interceptor();
            Assert.AreEqual(1, missileBoat.YellowDice);

            var cannon = missileBoat.Components.FirstOrDefault(x => x.Name == "Ion Cannon");
            var replace = missileBoat.Replace(cannon, new PlasmaMissile());
            
            Assert.AreEqual(0, missileBoat.YellowDice);
        }
    }
}
