using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL;
using Occultation.DAL.EF;

namespace Occultation.DataModels
{
    public class TakingTurns
    {
        public ITurnRepository Repo { get; set; }
        private int _playerId;
        private int _gameId;

        public TakingTurns()
        {
            Repo = new TurnRepository();
        }

        public MapTile Explore(string name, string game, int division)
        {
            var gameThings = Repo.GetPlayerAndGameIds(name, game);
            _playerId = gameThings.Item1;
            _gameId = gameThings.Item2;



            
            
            return new MapTile();
        }
    }
}
