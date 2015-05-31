using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occultation.DAL.EF
{
    public class GameDiscovery
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int DiscoveryId { get; set; }
        public int SortOrder { get; set; }
        public bool Revealed { get; set; }
        public int PlayerId { get; set; }
        public bool? Claimed { get; set; }
        public int MapDeckId { get; set; }
    }
}
