using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class PlacedMapTile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Orientation { get; set; }
        public int MapId { get; set; }
        public int[] WormHoles { get; set; }
        public void SetOrientation()
        {
            
        }

    }
}
