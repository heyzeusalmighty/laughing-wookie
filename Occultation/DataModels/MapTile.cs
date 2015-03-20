using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DataModels
{
    public class MapTile
    {
        public int MapId { get; set; }
        public int? x { get; set; }
        public int? y { get; set; }
        //public int Orientation { get; set; }
        public int Division { get; set; }
        public int Orange { get; set; }
        public int Brown { get; set; }
        public int Pink { get; set; }
        public int OrangeAdvanced { get; set; }
        public int BrownAdvanced { get; set; }
        public int PinkAdvanced { get; set; }
        public int White { get; set; }
        public int VictoryPoints { get; set; }
        public int[] Wormholes { get; set; }
        public int Aliens { get; set; }
        public string Occupied { get; set; }
        public bool Reward { get; set; }

    }




}
