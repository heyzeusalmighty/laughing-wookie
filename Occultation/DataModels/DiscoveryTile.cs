using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class DiscoveryTile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Discovery Discovery { get; set; }
        public int VictoryPoints { get; set; }
        public int ShipPart { get; set;  }

    }

    public enum Discovery
    {
        Money,
        Science,
        Material,
        AncientCruiser,
        AncientTechnology,
        AncientShipPart
    }

    public class AllDiscoveryTiles
    {
        public List<DiscoveryTile> Tiles { get; set; }

        public AllDiscoveryTiles()
        {
            Tiles = new List<DiscoveryTile>
            {
                new DiscoveryTile { Id = 1, Name = "Money", Discovery = Discovery.Money},
                new DiscoveryTile { Id = 2, Name = "Money", Discovery = Discovery.Money},
                new DiscoveryTile { Id = 3, Name = "Money", Discovery = Discovery.Money},

                new DiscoveryTile { Id = 4, Name = "Science", Discovery = Discovery.Science},
                new DiscoveryTile { Id = 5, Name = "Science", Discovery = Discovery.Science},
                new DiscoveryTile { Id = 6, Name = "Science", Discovery = Discovery.Science},

                new DiscoveryTile { Id = 7, Name = "Material", Discovery = Discovery.Material},
                new DiscoveryTile { Id = 8, Name = "Material", Discovery = Discovery.Material},
                new DiscoveryTile { Id = 9, Name = "Material", Discovery = Discovery.Material},

                new DiscoveryTile { Id = 10, Name = "Ancient Cruiser", Discovery = Discovery.AncientCruiser},
                new DiscoveryTile { Id = 11, Name = "Ancient Cruiser", Discovery = Discovery.AncientCruiser},
                new DiscoveryTile { Id = 12, Name = "Ancient Cruiser", Discovery = Discovery.AncientCruiser},

                new DiscoveryTile { Id = 13, Name = "Ancient Technology", Discovery = Discovery.AncientTechnology},
                new DiscoveryTile { Id = 14, Name = "Ancient Technology", Discovery = Discovery.AncientTechnology},
                new DiscoveryTile { Id = 15, Name = "Ancient Technology", Discovery = Discovery.AncientTechnology},
                
                new DiscoveryTile { Id = 16, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1},
                new DiscoveryTile { Id = 17, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1},
                new DiscoveryTile { Id = 18, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1},
                new DiscoveryTile { Id = 19, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1},
                new DiscoveryTile { Id = 20, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1},
                new DiscoveryTile { Id = 21, Name = "Ancient Ship Part", Discovery = Discovery.AncientShipPart, ShipPart = 1}
            };
            Tiles.Shuffle();
        }



    }
}
