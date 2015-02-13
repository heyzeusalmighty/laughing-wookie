using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Occultation.DataModels
{
    public class Ship
    {
        public Guid ShipId { get; set; }

        public List<ShipComponent> Components { get; set; }
        public IEnumerable<ShipPositions> TotalCompartments { get; set; }
        
        public int ShipInitiative { get; set; }
        public int Compartments { get; set; }
        public int PowerConsumption { get; set; }
        public int PowerAvailable { get; set; }
        public int Computers { get; set; }
        public int HullPoints { get; set; }
        public int Initiative { get; set; }
        public int Missiles { get; set; }
        public int OrangeDice { get; set; }
        public int YellowDice { get; set; }
        public int RedDice { get; set; }
        public int Shields { get; set; }
        public int Drive { get; set; }
        public ShipStatus Status = ShipStatus.Active;
        public int Damage { get; set; }
        public int Value { get; set; }
        

        public int AvailableCompartments()
        {
            var slots = Components.Count();
            return Compartments - slots;
        }

        public bool EnoughPower(int power)
        {
            return ((PowerConsumption + power) <= PowerAvailable);
        }

        public void Initialize(ShipComponent tile)
        {
            if (Components == null) Components = new List<ShipComponent>();
            Components.Add(tile);
            PowerAvailable += tile.Power;
            Computers += tile.Computers;
            HullPoints += tile.HullPoints;
            Initiative += tile.Initiative;
            Missiles += tile.Missiles;
            OrangeDice += tile.OrangeDice;
            RedDice += tile.RedDice;
            Shields += tile.Shields;
            YellowDice += tile.YellowDice;
            PowerConsumption += tile.PowerCost;
            Drive += tile.Drive;
        }
        public bool Upgrade(ShipComponent tile)
        {
            if ((AvailableCompartments() > 0) && (EnoughPower(tile.PowerCost) || tile.Power > 0))
            {
                Components.Add(tile);
                PowerAvailable += tile.Power;
                Computers += tile.Computers;
                HullPoints += tile.HullPoints;
                Initiative += tile.Initiative;
                Missiles += tile.Missiles;
                OrangeDice += tile.OrangeDice;
                RedDice += tile.RedDice;
                Shields += tile.Shields;
                YellowDice += tile.YellowDice;
                PowerConsumption += tile.PowerCost;
                Drive += tile.Drive;
                return true;
            }
            return false;
        }

        public bool Replace(ShipComponent original, ShipComponent replacement)
        {
            //Deal with Power Consumption
            
            if ((original.Power > 0 && replacement.Power > 0) && (PowerAvailable - original.Power + replacement.Power >= PowerConsumption))
            {
                Components.Remove(original);
                Upgrade(replacement);
                return true;
            }

            if ((original.Drive > 0 && replacement.Drive > 0) || original.Drive == 0)
            {
                var newPower = PowerConsumption - original.PowerCost;
                if (newPower + replacement.PowerCost <= PowerAvailable)
                {
                    var removed = false;
                    foreach (var comp in Components)
                    {
                        if (comp.Name == original.Name)
                        {
                            removed = true;
                            Components.Remove(comp);
                            PowerAvailable -= comp.Power;
                            Computers -= comp.Computers;
                            HullPoints -= comp.HullPoints;
                            Initiative -= comp.Initiative;
                            Missiles -= comp.Missiles;
                            OrangeDice -= comp.OrangeDice;
                            RedDice -= comp.RedDice;
                            Shields -= comp.Shields;
                            YellowDice -= comp.YellowDice;
                            PowerConsumption -= comp.PowerCost;
                            Drive -= comp.Drive;
                            break;
                        }
                    }
                    if (removed)
                    {
                        Upgrade(replacement);
                        return true;
                    }
                    return false;
                }
            }
            
            return false;
        }

        public bool WillBeDestroyed(int hit)
        {
            var currentDamage = Damage;
            return (hit + currentDamage >= HullPoints);

        }
        public bool DetermineHit(int roll, int computer)
        {
            if (roll == 6)
            {
                return true;
            }

            if (roll == 1)
            {
                return false;
            }

            if ((roll + computer - Shields) >= 6)
            {
                return true;
            }

            return false;
        }
    }


    public class Interceptor : Ship
    {
        public Interceptor()
        {
            ShipId = Guid.NewGuid();
            TotalCompartments = new List<ShipPositions>
            {
                ShipPositions.Port1,
                ShipPositions.Starboard1,
                ShipPositions.Bow,
                ShipPositions.Stern
            };
            Compartments = TotalCompartments.Count();
            Initiative = 2;
            Value = 5;
            Initialize(new NuclearSource());
            Initialize(new NuclearDrive());
            Initialize(new IonCannon());
        }

    }

    public class Cruiser : Ship
    {
        public Cruiser()
        {
            ShipId = Guid.NewGuid();
            TotalCompartments = new List<ShipPositions>
            {
                ShipPositions.Port1,
                ShipPositions.Port2,
                ShipPositions.Starboard1,
                ShipPositions.Starboard2,
                ShipPositions.Bow,
                ShipPositions.Stern
            };
            Compartments = TotalCompartments.Count();
            Initiative = 1;
            Initialize(new NuclearSource());
            Initialize(new NuclearDrive());
            Initialize(new IonCannon());
            Initialize(new Hull());
            Initialize(new ElectronComputer());
        }
    }

    public class Ancient : Ship
    {
        public Ancient()
        {
            ShipId = Guid.NewGuid();
            Initiative = 2;
            HullPoints = 1;
            Computers = 1;
            YellowDice = 2;
        }
    }
    
    public enum ShipPositions
    {
        Bow,
        Middle,
        Stern,
        Port1,
        Port2,
        Port3,
        Starboard1,
        Starboard2,
        Starboard3
    }

    public class ShipComponent
    {
        public int ComponentId { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int PowerCost { get; set; }
        public ShipPositions Position { get; set; }
        public int HullPoints { get; set; }
        public int YellowDice { get; set; }
        public int OrangeDice { get; set; }
        public int RedDice { get; set; }
        public int Missiles { get; set; }
        public int Shields { get; set; }
        public int Computers { get; set; }
        public int Drive { get; set; }
        public int Initiative { get; set; }
    }

    public enum ShipStatus
    {
        Active,
        Done,
        Destroyed
    }

}
