using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;

using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Occultation.DataModels;
using Assert = NUnit.Framework.Assert;


namespace Occult_Tests.Tests
{
    [TestClass]
    public class BattleTests
    {
        [TestMethod]
        public void DiceRoll_NotOverSix()
        {
            var counter = 10000;
            var results = new List<int>();
            while (counter > 1)
            {
                var roll = new DiceRoll();
                results.Add(roll.RollTheDice());
                counter--;
            }

            var one = 0;
            var two = 0;
            var three = 0;
            var four = 0;
            var five = 0;
            var six = 0;
            var unknown = 0;


            foreach (var roll in results)
            {
                Assert.LessOrEqual(roll, 6);
                switch (roll)
                {
                    case 1:
                        one++;
                        break;
                    case 2:
                        two++;
                        break;
                    case 3:
                        three++;
                        break;
                    case 4:
                        four++;
                        break;
                    case 5:
                        five++;
                        break;
                    case 6:
                        six++;
                        break;
                    default:
                        unknown++;
                        break;

                }


            }

            Console.WriteLine("one   => " + one);
            Console.WriteLine("two   => " + two);
            Console.WriteLine("three => " + three);
            Console.WriteLine("four  => " + four);
            Console.WriteLine("five  => " + five);
            Console.WriteLine("six   => " + six);
            Console.WriteLine("??    => " + unknown);





        }

        [TestMethod]
        public void Battle_MissileAttack()
        {
            var boring = CreateBoringFleet();
            boring.AddShip(new Interceptor());
            boring.AddShip(new Interceptor());
            
            
            var battle = new Battle(CreateMissileFleet(), boring, new DiceRoll("hit"));
            
            
            battle.MissileLineUp();
            Assert.AreEqual(1, battle.Attacker.ActiveShips.Count);
        }

        [TestMethod]
        public void Battle_MissileIsCleared()
        {
            var battle = new Battle(CreateMissileFleet(), CreateMissileFleet(), new DiceRoll("hit"));
            battle.MissileLineUp();
            Assert.IsFalse(battle.Attacker.ActiveShips.Any(x => x.Missiles > 0));

            Assert.AreEqual(3, battle.Defender.ActiveShips.Count);
        }

        [TestMethod]
        public void Battle_DefenderFiresMissilesFirst()
        {
            var battle = new Battle(CreateOneMissileShipFleet(), CreateOneMissileShipFleet(), new DiceRoll("hit"));
            
            battle.MissileLineUp();
            
            Assert.AreEqual(1, battle.Defender.ActiveShips.Count);
            Assert.AreEqual(0, battle.Attacker.ActiveShips.Count);
            Assert.AreEqual(1, battle.Attacker.DestroyedShips.Count);
        }

        [TestMethod]
        public void Battle_DefenderFiresFirstAndDoesDamage()
        {
            var battle = new Battle(
                new Fleet(new List<Ship>{ new Interceptor()}), new Fleet(new List<Ship>{ new Interceptor()}), new DiceRoll("hit")
                );

            battle.CannonLineUp();
            battle.ShootShootPewPew();

            Assert.AreEqual(0, battle.Defender.ActiveShips.Count);
            Assert.AreEqual(1, battle.Defender.DoneShips.Count);
            Assert.AreEqual(0, battle.Attacker.ActiveShips.Count);
            Assert.AreEqual(1, battle.Attacker.DestroyedShips.Count);

            //var attackerShip = battle.Attacker.DoneShips.First();
            //Assert.AreEqual(1, attackerShip.Damage);

        }

        [TestMethod]
        public void Battle_InRegularBattle_DefenderGoesFirst()
        {
            var battle = new Battle(CreateBoringFleet(), CreateBoringFleet(), new DiceRoll("hit"));
        }

        [TestMethod]
        public void Battle_CheckOrderOfCombat_CannonVolley()
        {
            var def = CreateSlightlyBetterFleet();
            var att = CreateSlightlyWorseFleet();
            var battle = new Battle(def, att, new DiceRoll());

            battle.CannonLineUp();

            Assert.AreEqual(6, battle.CannonFodder.Count);
            var first = battle.CannonFodder[0];
            var second = battle.CannonFodder[1];
            var third = battle.CannonFodder[2];
            var fourth = battle.CannonFodder[3];
            var fifth = battle.CannonFodder[4];
            var sixth = battle.CannonFodder[5];

            Assert.AreEqual("Defender", first.Side);
            Assert.AreEqual("Defender", second.Side);
            Assert.AreEqual("Attacker", third.Side);
            Assert.AreEqual("Defender", fourth.Side);
            Assert.AreEqual("Attacker", fifth.Side);
            Assert.AreEqual("Attacker", sixth.Side);

        }

        

        [TestMethod]
        public void Battle_ResetDoneShips()
        {
            var def = CreateSlightlyBetterFleet();
            var att = CreateSlightlyWorseFleet();
            var battle = new Battle(def, att, new DiceRoll());

            //Take all the defenders ships and stick em in the Done
            var selected = battle.Defender.ActiveShips.ToList();
            selected.ForEach(item => battle.Defender.ActiveShips.Remove(item));
            battle.Defender.DoneShips.AddRange(selected);

            battle.ResetDoneShips();
            Assert.AreEqual(0, battle.Defender.DoneShips.Count);
            Assert.AreEqual(3, battle.Defender.ActiveShips.Count);

        }

        [TestMethod]
        public void Battle_ResetDestroyShips()
        {
            var def = CreateSlightlyBetterFleet();
            var att = CreateSlightlyWorseFleet();
            var battle = new Battle(def, att, new DiceRoll());

            //Take all the defenders ships and stick em in the Destroyed
            var destroyed = battle.Defender.ActiveShips.ToList();
            destroyed.ForEach(item => battle.Defender.DestroyedShips.Add(item));
            battle.Defender.ActiveShips.Clear();

            Assert.AreEqual(0, battle.DestroyedShips.Count);
            
            battle.ResetDestroyedShips();

            Assert.AreEqual(3, battle.DestroyedShips.Count);
        }


        [TestMethod]
        public void Battle_MoveToDone()
        {
            var def = CreateSlightlyBetterFleet();
            var att = CreateSlightlyWorseFleet();
            var battle = new Battle(def, att, new DiceRoll());

            Assert.AreEqual(0, battle.Defender.DoneShips.Count);
            var firstDef = battle.Defender.ActiveShips[0];

            battle.MoveShipToDone(firstDef.ShipId, "Defender");

            Assert.AreEqual(1, battle.Defender.DoneShips.Count);
            Assert.AreEqual(2, battle.Defender.ActiveShips.Count);
        }


        [TestMethod]
        public void Battle_DoOneFullRound()
        {

            var def = CreateSlightlyBetterFleet();
            var att = CreateSlightlyWorseFleet();
            var battle = new Battle(def, att, new DiceRoll("miss"));
            
            
            battle.CannonLineUp();
            battle.ShootShootPewPew();

            Assert.AreEqual(3, battle.Defender.DoneShips.Count);
            Assert.AreEqual(3, battle.Attacker.DoneShips.Count);
            Assert.AreEqual(0, battle.Defender.ActiveShips.Count);
            Assert.AreEqual(0, battle.Attacker.ActiveShips.Count);
            
            battle.ResetDoneShips();
            Assert.AreEqual(3, battle.Defender.ActiveShips.Count);
            Assert.AreEqual(3, battle.Attacker.ActiveShips.Count);
            Assert.AreEqual(0, battle.Defender.DoneShips.Count);
            Assert.AreEqual(0, battle.Attacker.DoneShips.Count);
        }

        [TestMethod]
        public void Battle_VictoryIsAssured()
        {
            var def = CreateSlightlyBetterFleet();
            var att = CreateMissileFleet();

            var battle = new Battle(def, att, new DiceRoll());

            battle.CommenceBattle();

            bool someoneOne = battle.Attacker.FleetDestroyed() || battle.Defender.FleetDestroyed();

            Assert.IsTrue(someoneOne);
        }

        [TestMethod]
        public void Battle_OneThousandRounds()
        {
            
            var counter = 0;
            var defWins = 0;
            var attWins = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (counter < 10000)
            {
                var def = CreateSlightlyWorseFleet();
                var att = CreateSlightlyBetterFleet();
                var battle = new Battle(def, att, new DiceRoll());
                if (battle.DidDefendersNotDefend())
                {
                    attWins++;
                }
                else
                {
                    defWins++;
                }
                Console.WriteLine(counter++);
                Console.Clear();
            }
            stopwatch.Stop();

            
            Console.WriteLine("ReSULTS");
            Console.WriteLine("Defenders : " + defWins);
            Console.WriteLine("Attackers : " + attWins);
            Console.WriteLine("Took : " + stopwatch.Elapsed);
            Assert.IsTrue(true);

        }
        


    public Battle MissileBattle()
        {
            return new Battle(CreateMissileFleet(), CreateBoringFleet(), new DiceRoll("hit"));
        }
        public Fleet CreateMissileFleet()
        {   
            return new Fleet(new List<Ship>
            {
                CreateMissileBoat(), CreateMissileBoat(), new Interceptor()
            });
        }

        public Fleet CreateOneMissileShipFleet()
        {
            return new Fleet(new List<Ship>
            {
                CreateMissileBoat() 
            });
        }

        public Fleet CreateBoringFleet()
        {
            return new Fleet(new List<Ship>
            {
                new Interceptor(), new Interceptor(), new Interceptor()
            });
        }

        public Fleet CreateSlightlyBetterFleet()
        {
            var four = new Interceptor();
            var comp = four.Upgrade(new PositronComputer());
            return new Fleet
                (new List<Ship>
                {
                    new Cruiser(), new Interceptor(), four
                });
        }

        public Fleet CreateSlightlyWorseFleet()
        {
            return new Fleet(new List<Ship>{ new Cruiser(), new Cruiser(), new Interceptor()});
        }

        public Ship CreateMissileBoat()
        {
            var missileBoat = new Interceptor();
            var cannon = missileBoat.Components.FirstOrDefault(x => x.Name == "Ion Cannon");
            var replace = missileBoat.Replace(cannon, new PlasmaMissile());
            return missileBoat;
        }

    }
}
