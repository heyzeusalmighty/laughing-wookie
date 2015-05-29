using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class ExploratoryPackage
    {
        public MapTile Tile { get; set; }
        public DiscoveryTile Reward { get; set; }
        public string Message { get; set; }
    }
}
