using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class Battle
    {

        public Fleet Defender { get; set; }
        public Fleet Attacker { get; set; }
        public DiceRoll Dice { get; set; }
        public List<CannonVolley> CannonFodder { get; set; }
        public List<Guid> DestroyedShips { get; set; }
        public int Counter { get; set; }

        


        // if three people enter one hex
        // the second and third person fight first
        // then the first person fights the winner of that battle

        // Defender gets advantage if initiative is the same
        // Plasma Missiles go first
        // Who has initiative
        // if the ship is destroyed, they can't fire



        public Battle(Fleet defender, Fleet attacker, DiceRoll rollin)
        {
            Defender = defender;
            Attacker = attacker;
            Dice = rollin;
            DestroyedShips = new List<Guid>();
            CannonFodder = new List<CannonVolley>();
            Counter = 0;

            // Missile Volley
            // 1. Determine who's firing missiles
            // 2. Get initiative
            // 3. First Volley - If Hits, use algorithm to destroy
            // 4. Second Volley - If Hits, use algorithm to destroy

        }

        public void CommenceBattle()
        {
            MissileLineUp();

            //Normal Combat
            CombatLoop();
        }

        public bool DidDefendersNotDefend()
        {
            while (!BattleHasConcluded())
            {
                Counter++;
                Console.WriteLine("Round " + Counter);
                CannonFodder.Clear();
                CannonLineUp();
                ShootShootPewPew();
                ResetDoneShips();
            }

            return Defender.FleetDestroyed();
        }

        public void CombatLoop()
        {
            
            while (!BattleHasConcluded())
            {
                Counter++;
                Console.WriteLine("Round " + Counter);
                CannonFodder.Clear();
                CannonLineUp();
                ShootShootPewPew();
                ResetDoneShips();

                
            }
            Console.WriteLine("Its Done");

            if (Defender.FleetDestroyed())
            {
                Console.WriteLine("Attackers won with {0} ships left & {1} ships destroyed",
                    Attacker.ActiveShips.Count, Attacker.DestroyedShips.Count);
            }
            else
            {
                Console.WriteLine("Defenders won with {0} ships left & {1} ships destroyed",
                    Defender.ActiveShips.Count, Defender.DestroyedShips.Count);
            }

        }

        public void CannonLineUp()
        {
            CannonFodder = new List<CannonVolley>();
            var defCannons = Defender.ActiveShips.Select(y => new CannonVolley
            {
                Side = "Defender",
                YellowDice = y.YellowDice,
                OrangeDice = y.OrangeDice,
                RedDice = y.RedDice,
                Initiative = y.Initiative,
                ShipId = y.ShipId,
                Computer = y.Computers
            }).ToList();
            CannonFodder.AddRange(defCannons);

            var attackCannons = Attacker.ActiveShips.Select(y => new CannonVolley
            {
                Side = "Attacker",
                YellowDice = y.YellowDice,
                OrangeDice = y.OrangeDice,
                RedDice = y.RedDice,
                Initiative = y.Initiative,
                ShipId = y.ShipId,
                Computer = y.Computers
            }).ToList();
            CannonFodder.AddRange(attackCannons);

            //This isn't pretty but it sorts by both Initiative and Side  (defense goes first)
            CannonFodder = CannonFodder.OrderBy(m => m.Initiative.ToString() + m.Side).Reverse().ToList();
        }

        public void ShootShootPewPew()
        {
            
            foreach (var volley in CannonFodder)
            {
                if (!DestroyedShips.Contains(volley.ShipId))
                {
                    var yellowCount = volley.YellowDice;
                    for (var y = 0; y < yellowCount; y++)
                    {
                        var rull = Dice.RollTheDice();
                            
                        if (volley.Side == "Defender")
                        {
                            if (Attacker.DoTheDamage(1, rull, volley.Computer))
                            {
                                Console.WriteLine("Defender scored hit for 1");
                                ResetDestroyedShips();
                            }
                        }
                        else
                        {
                            if (Defender.DoTheDamage(1, rull, volley.Computer))
                            {
                                Console.WriteLine("Attacker scored hit for 1");
                                ResetDestroyedShips();
                            }
                        }
                    }

                    var orangeCount = volley.OrangeDice;
                    for (var y = 0; y < orangeCount; y++)
                    {
                        var rull = Dice.RollTheDice();

                        if (volley.Side == "Defender")
                        {
                            if (Attacker.DoTheDamage(2, rull, volley.Computer))
                            {
                                Console.WriteLine("Defender scored hit for 2");
                                ResetDestroyedShips();
                            }
                        }
                        else
                        {
                            if (Defender.DoTheDamage(2, rull, volley.Computer))
                            {
                                Console.WriteLine("Attacker scored hit for 2");
                                ResetDestroyedShips();
                            }
                        }
                    }

                    var redCount = volley.RedDice;
                    for (var y = 0; y < redCount; y++)
                    {
                        var rull = Dice.RollTheDice();

                        if (volley.Side == "Defender")
                        {
                            if (Attacker.DoTheDamage(4, rull, volley.Computer))
                            {
                                Console.WriteLine("Defender scored hit for 4");
                                ResetDestroyedShips();
                            }
                        }
                        else
                        {
                            if (Defender.DoTheDamage(4, rull, volley.Computer))
                            {
                                Console.WriteLine("Attacker scored hit for 4");
                                ResetDestroyedShips();
                            }
                        }
                    }
                    MoveShipToDone(volley.ShipId, volley.Side);
                }
            }

            
        }

        public void MissileLineUp()
        {
            var defMissiles = Defender.ActiveShips.Where(x => x.Missiles > 0).Select(y => new MissileVolley
            {
                Side = "Defender",
                Missiles = y.Missiles,
                Initiative = y.Initiative,
                ShipId = y.ShipId,
                Computer = y.Computers
            })
            .OrderByDescending(x => x.Initiative).ToList();

            var attackMissiles = Attacker.ActiveShips.Where(x => x.Missiles > 0).Select(y => new MissileVolley
            {
                Side = "Attacker",
                Missiles = y.Missiles,
                Initiative = y.Initiative,
                ShipId = y.ShipId,
                Computer = y.Computers
            }).OrderByDescending(x => x.Initiative).ToList();
            
            defMissiles.AddRange(attackMissiles);
            MissileLaunching(defMissiles.OrderByDescending(x => x.Initiative).ToList());
        }
        
        public void MissileLaunching(List<MissileVolley> volleys)
        {
            foreach (var volley in volleys)
            {
                //first check if this ship is still around
                if (!DestroyedShips.Contains(volley.ShipId))
                {
                    //you get two rolls per missile
                    var missileCount = volley.Missiles;
                    while (missileCount > 0)
                    {
                        var rull = Dice.RollTheDice();
                        Console.WriteLine("{0} rolled {1} for Missiles", volley.Side, rull);

                        if (volley.Side == "Defender")
                        {
                            if (Attacker.DoTheDamage(2, rull, volley.Computer))
                            {
                                Console.WriteLine("Defense fired off missiles");
                                ResetDestroyedShips();
                            }
                        }
                        else
                        {
                            if (Defender.DoTheDamage(2, rull, volley.Computer))
                            {
                                Console.WriteLine("Attackers fired off missiles");
                                ResetDestroyedShips();
                            }
                        }

                        missileCount--;
                    }
                }

                
                
            }
        }

        public void DefenderMissileSuccess()
        {
            Console.WriteLine("Defense fired off missiles");
            if (!Attacker.Done())
            {
                //var first = Attacker.ActiveShips[0];
                if (Attacker.ActiveShips.Any(x => x.Missiles > 0))
                {
                    var first = Attacker.ActiveShips.FirstOrDefault(x => x.Missiles > 0);
                    Attacker.DestroyedShips.Add(first);
                    Attacker.ActiveShips.Remove(first);
                    DestroyedShips.Add(first.ShipId);
                }
                else
                {
                    var first = Attacker.ActiveShips[0];
                    Attacker.DestroyedShips.Add(first);
                    Attacker.ActiveShips.Remove(first);
                    DestroyedShips.Add(first.ShipId);
                }
                
            }
            
        }

        public void AttackerMissileSuccess()
        {
            Console.WriteLine("Attacker fired off missiles");
            if (!Defender.Done())
            {
                if (Defender.ActiveShips.Any(x => x.Missiles > 0))
                {
                    var first = Defender.ActiveShips.FirstOrDefault(x => x.Missiles > 0);
                    Defender.DestroyedShips.Add(first);
                    Defender.ActiveShips.Remove(first);
                    DestroyedShips.Add(first.ShipId);
                }
                else
                {
                    var first = Defender.ActiveShips[0];
                    Defender.DestroyedShips.Add(first);
                    Defender.ActiveShips.Remove(first);
                    DestroyedShips.Add(first.ShipId);
                }
                
            }
        }

        public void ResetDestroyedShips()
        {
            DestroyedShips.Clear();
            DestroyedShips = Defender.DestroyedShips.Select(x => x.ShipId).ToList();
            DestroyedShips.AddRange(Attacker.DestroyedShips.Select(x => x.ShipId));
        }

        public void ResetDoneShips()
        {
            foreach (var def in Defender.DoneShips)
            {
                Defender.ActiveShips.Add(def);
            }
            Defender.DoneShips.Clear();

            foreach (var att in Attacker.DoneShips)
            {
                Attacker.ActiveShips.Add(att);
            }
            Attacker.DoneShips.Clear();
        }

        public void MoveShipToDone(Guid shipId , string side )
        {
            if (side == "Defender")
            {
                var ship = Defender.ActiveShips.FirstOrDefault(x => x.ShipId == shipId);
                if (ship != null)
                {
                    Defender.DoneShips.Add(ship);
                    Defender.ActiveShips.Remove(ship);
                }

            }
            else
            {
                var ship = Attacker.ActiveShips.FirstOrDefault(x => x.ShipId == shipId);
                if (ship != null)
                {
                    Attacker.DoneShips.Add(ship);
                    Attacker.ActiveShips.Remove(ship);
                }
            }
        }

        public bool BattleHasConcluded()
        {
            return (Defender.FleetDestroyed() || Attacker.FleetDestroyed());
        }
    }


    public class MissileVolley : Volley
    {
        public int Missiles { get; set; }
        //public Ship Ship { get; set; }
    }

    public class Volley
    {
        public string Side { get; set; }
        public int Computer { get; set; }
        public int Initiative { get; set; }
        public Guid ShipId { get; set; }
    }

    public class CannonVolley : Volley
    {
        public int YellowDice { get; set; }
        public int OrangeDice { get; set; }
        public int RedDice { get; set; }
    }
    

}
