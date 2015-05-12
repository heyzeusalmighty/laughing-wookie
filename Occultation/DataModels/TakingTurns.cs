using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL;

namespace Occultation.DataModels
{
    public class TakingTurns
    {
        public ITurnRepository Repo { get; set; }

        public TakingTurns()
        {
            Repo = new TurnRepository();
        }

        public MapTile Explore(string name, int division)
        {
            
        }
    }
}
