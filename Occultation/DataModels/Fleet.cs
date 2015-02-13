using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class Fleet
    {
        public List<Ship> ActiveShips { get; set; }
        public List<Ship> DoneShips { get; set; }
        public List<Ship> DestroyedShips { get; set; }


        public Fleet(List<Ship> ships)
        {
            ActiveShips = ships;
            DestroyedShips = new List<Ship>();
            DoneShips = new List<Ship>();
            SortByInitiative();
        }


        public void AddShip(Ship ship)
        {
            ActiveShips.Add(ship);
            SortByInitiative();
        }

        private void SortByInitiative()
        {
            ActiveShips = ActiveShips.OrderByDescending(o => o.Initiative).ToList();
        }

        public Ship GetNextAttacker()
        {
            if (ActiveShips.Any())
            {
                var ship = ActiveShips[0];
                ActiveShips.RemoveAt(0);
                ship.Status = ShipStatus.Done;
                DoneShips.Add(ship);
                return ship;
            }
            else
            {
                return null;
            }
        }

        public int GetCurrentInitiative()
        {
            if (ActiveShips.Any())
            {
                return ActiveShips[0].Initiative;
            }
            return -1;
        }

        public bool Done()
        {
            return ActiveShips.Count == 0;
        }

        public bool FleetDestroyed()
        {
            return (ActiveShips.Count == 0 && DoneShips.Count == 0 && DestroyedShips.Count > 0);
        }

        public bool DetermineDamage(int damage, int roll, int computer)
        {
            int shipSize = -1;
            bool destroyed = false;
            Ship hitShip = null;

            //automatic misses on 1
            if (roll == 1)
                return false;

            foreach (var ship in ActiveShips)
            {
                if (ship.DetermineHit(roll, computer))
                {
                    if (destroyed)
                    {
                        if (ship.Damage + damage > ship.HullPoints)
                        {
                            if (ship.Compartments > shipSize)
                            {
                                hitShip = ship;
                                shipSize = ship.Compartments;
                            }
                        }
                    }
                    else
                    {
                        if (ship.Damage + damage > ship.HullPoints)
                        {
                            hitShip = ship;
                            shipSize = ship.Compartments;
                            destroyed = true;
                        }
                        else
                        {
                            if (ship.Compartments > shipSize)
                            {
                                
                            }
                        }



                    }
                }


                

            }


            foreach (var ship in DoneShips)
            {
                if (ship.DetermineHit(roll, computer))
                {
                    if (destroyed)
                    {
                        if (ship.Damage + damage > ship.HullPoints)
                        {
                            if (ship.Compartments > shipSize)
                            {
                                hitShip = ship;
                                shipSize = ship.Compartments;
                            }
                        }
                    }
                    else
                    {
                        if (ship.Damage + damage > ship.HullPoints)
                        {
                            hitShip = ship;
                            shipSize = ship.Compartments;
                            destroyed = true;
                        }
                        else
                        {
                            if (ship.Compartments > shipSize)
                            {

                            }
                        }



                    }
                }
            }
            return true;
        }

        public bool DoTheDamage(int damage, int roll, int computer)
        {
            //automatic misses on 1
            if (roll == 1)
                return false;

            var activeDestroys = new List<Ship>();
            var itsTheHits = new List<Ship>();
            var doneDestroys = new List<Ship>();

            foreach (var ship in ActiveShips)
            {
                if (ship.DetermineHit(roll, computer))
                {
                    itsTheHits.Add(ship);
                    if (ship.WillBeDestroyed(damage))
                    {
                        activeDestroys.Add(ship);
                    }
                }
            }

            foreach (var ship in DoneShips)
            {
                if (ship.DetermineHit(roll, computer))
                {
                    itsTheHits.Add(ship);
                    if (ship.WillBeDestroyed(damage))
                    {
                        doneDestroys.Add(ship);
                    }
                }
            }

            if (activeDestroys.Any())
            {
                activeDestroys.OrderByDescending(x => x.Value).ToList();
                var destroyed = activeDestroys[0];
                DestroyedShips.Add(destroyed);
                var toBeRemoved = ActiveShips.FirstOrDefault(x => x.ShipId == destroyed.ShipId);
                if (toBeRemoved != null)
                {
                    ActiveShips.Remove(toBeRemoved);
                    Console.WriteLine("Active Ship Destroyed");
                    return true;
                }
            }

            if (doneDestroys.Any())
            {
                doneDestroys.OrderByDescending(x => x.Value).ToList();
                var destroyed = doneDestroys[0];
                DestroyedShips.Add(destroyed);
                var toBeRemoved = DoneShips.FirstOrDefault(x => x.ShipId == destroyed.ShipId);
                if (toBeRemoved != null)
                {
                    DoneShips.Remove(toBeRemoved);
                    Console.WriteLine("Done Ship Destroyed");
                    return true;
                }
            }

            if (itsTheHits.Any())
            {
                itsTheHits.OrderByDescending(x => x.Value).ToList();
                var damaged = itsTheHits[0];
                damaged.Damage = damage;
                Console.WriteLine("Damage done");
            }



            return false;
        }
    }

}
