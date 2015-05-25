using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{

    public class AllUpgrades
    {
        public IEnumerable<ShipComponent> Components { get; set; }

        public AllUpgrades()
        {
            Components= new List<ShipComponent>
            {
                new IonCannon(),
                new Hull(),
                new ElectronComputer(),
                new NuclearSource(),
                new NuclearDrive(),
                new IonTurret(),
                new AxionComputer(),
                new FluxShield(),
                new ConformalDrive(),
                new ShardHull(),
                new HyperGridSource(),
                new PlasmaCannon(),
                new PlasmaMissile(),
                new PositronComputer(),
                new GluonComputer(),
                new AntiMatterCannon(),
                new ImprovedHull(),
                new GaussShield(),
                new PhaseShield(),
                new FusionDrive(),
                new TachyonDrive(),
                new FusionSource(),
                new TachyonSource()
            };
        }

    }
        

        public class IonCannon : ShipComponent
        {
            public IonCannon()
            {
                ComponentId = 1;
                Name = "Ion Cannon";
                YellowDice = 1;
                PowerCost = 1;
            }
        }

        public class Hull : ShipComponent
        {
            public Hull()
            {
                ComponentId = 2;
                HullPoints = 1;
            }
        }

        public class ElectronComputer : ShipComponent
        {
            public ElectronComputer()
            {
                Name = "Electron Computer";
                Computers = 1;
                ComponentId = 3;
            }
        }

        public class NuclearSource : ShipComponent
        {
            public NuclearSource()
            {
                Name = "Nuclear Source";
                Power = 3;
                ComponentId = 4;
            }
        }

        public class NuclearDrive : ShipComponent
        {
            public NuclearDrive()
            {
                Name = "Nuclear Drive";
                Drive = 1;
                PowerCost = 1;
                ComponentId = 5;
            }
        }

        public class IonTurret : ShipComponent
        {
            public IonTurret()
            {
                Name = "Ion Turret";
                YellowDice = 2;
                PowerCost = 1;
                ComponentId = 6;
            }
        }

        public class AxionComputer : ShipComponent
        {
            public AxionComputer()
            {
                Name = "Axion Computer";
                Computers = 3;
                ComponentId = 7;
            }
        }

        public class FluxShield : ShipComponent
        {
            public FluxShield()
            {
                Shields = 3;
                Name = "Flux Shield";
                PowerCost = 2;
                ComponentId = 8;
            }
        }

        public class ConformalDrive : ShipComponent
        {
            public ConformalDrive()
            {
                Name = "Conformal Drive";
                Drive = 4;
                Initiative = 2;
                PowerCost = 2;
                ComponentId = 9;
            }
        }

        public class ShardHull : ShipComponent
        {
            public ShardHull()
            {
                Name = "Shard Hull";
                HullPoints = 3;
                ComponentId = 10;
            }
        }

        public class HyperGridSource : ShipComponent
        {
            public HyperGridSource()
            {
                Power = 11;
                Name = "Hyper Grid Source";
                ComponentId = 11;
            }
        }

        public class PlasmaCannon : ShipComponent
        {
            public PlasmaCannon()
            {
                Name = "Plasma Cannon";
                OrangeDice = 1;
                PowerCost = 2;
                ComponentId = 12;
            }
        }

        public class PlasmaMissile : ShipComponent
        {
            public PlasmaMissile()
            {
                Name = "Plasma Missile";
                Missiles = 2;
                ComponentId = 13;
            }
        }

        public class PositronComputer : ShipComponent
        {
            public PositronComputer()
            {
                Name = "Positron Computer";
                Computers = 2;
                Initiative = 1;
                PowerCost = 1;
                ComponentId = 14;
            }
        }

        public class GluonComputer : ShipComponent
        {
            public GluonComputer()
            {
                Name = "Gluon Computer";
                Computers = 3;
                Initiative = 2;
                PowerCost = 2;
                ComponentId = 15;
            }
        }

        public class AntiMatterCannon : ShipComponent
        {
            public AntiMatterCannon()
            {
                Name = "Antimatter Cannon";
                RedDice = 1;
                PowerCost = 4;
                ComponentId = 16;
            }
        }

        public class ImprovedHull : ShipComponent
        {
            public ImprovedHull()
            {
                Name = "Improved Hull";
                HullPoints = 2;
                ComponentId = 17;
            }
        }

        public class GaussShield : ShipComponent
        {
            public GaussShield()
            {
                Name = "Gauss Shield";
                Shields = 1;
                ComponentId = 18;
            }
        }

        public class PhaseShield : ShipComponent
        {
            public PhaseShield()
            {
                Name = "Phase Shield";
                Shields = 2;
                PowerCost = 1;
                ComponentId = 19;
            }
        }

        public class FusionDrive : ShipComponent
        {
            public FusionDrive()
            {
                Name = "Fusion Drive";
                Drive = 2;
                PowerCost = 2;
                Initiative = 2;
                ComponentId = 20;
            }
        }

        public class TachyonDrive : ShipComponent
        {
            public TachyonDrive()
            {
                Name = "Tachyon Drive";
                Drive = 3;
                Initiative = 3;
                PowerCost = 3;
                ComponentId = 21;
            }
        }

        public class FusionSource : ShipComponent
        {
            public FusionSource()
            {
                Power = 6;
                Name = "Fusion Source";
                ComponentId = 22;
            }
        }

        public class TachyonSource : ShipComponent
        {
            public TachyonSource()
            {
                Name = "Tachyon Source";
                Power = 9;
                ComponentId = 23;
            }
        }



    
}
