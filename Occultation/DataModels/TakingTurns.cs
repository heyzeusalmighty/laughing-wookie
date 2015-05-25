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


        public MapTile Explore(string name, string game, int division, int xCoords, int yCoords)
        {
            var gameThings = Repo.GetPlayerAndGameIds(name, game);

            if (gameThings.Item1 > 0 && gameThings.Item2 > 0)
            {
                _playerId = gameThings.Item1;
                _gameId = gameThings.Item2;

                var newTile = Repo.GetNewExploredMapTile(_gameId, xCoords, yCoords, _playerId, division);

                return newTile;
            }
            return null;
        }

        public List<Coordinates> GetExploreOptions(int x, int y, int playerId)
        {
            var explored = Repo.GetExploredTiles();


        }

        public Coordinates[] GetNeighbors(int x, int y)
        {
            if (x%2 == 0)
            {
                return new Coordinates[]
                {
                    new Coordinates { X = x, Y = y - 1 },
                    new Coordinates { X = x + 1, Y = y - 1 },
                    new Coordinates { X = x + 1, Y = y },
                    new Coordinates { X = x, Y = y + 1 },
                    new Coordinates { X = x - 1, Y = y },
                    new Coordinates { X = x - 1, Y = y - 1 }
                };
            }

            return new Coordinates[]
            {
                new Coordinates { X = x, Y = y - 1 },
                new Coordinates { X = x + 1, Y = y },
                new Coordinates { X = x + 1, Y = y + 1 },
                new Coordinates { X = x, Y = y + 1 },
                new Coordinates { X = x - 1, Y = y + 1 },
                new Coordinates { X = x - 1, Y = y }
            };
        

        }


        
    }


    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
