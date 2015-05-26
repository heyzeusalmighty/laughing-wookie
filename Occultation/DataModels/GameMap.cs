using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL.EF;

namespace Occultation.DataModels
{
    public class GameMap
    {
        public List<MapTile> MapTiles { get; set; }
        public RemainingMapCounts Counts { get; set; }
        public List<ShipForMap> Ships { get; set; }
        public List<Player> Players { get; set; }

    }
}
