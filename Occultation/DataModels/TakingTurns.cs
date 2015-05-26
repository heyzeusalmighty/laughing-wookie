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
        private List<MapTile> _exploredTiles;
        private List<MapTile> _availableTiles;

        public TakingTurns()
        {
            Repo = new TurnRepository();
            _availableTiles = new AvailableMapTile().AllTheTiles;
        }


        public MapTile Explore(string name, string game, int xCoords, int yCoords)
        {
            var gameThings = Repo.GetPlayerAndGameIds(name, game);

            if (gameThings.Item1 > 0 && gameThings.Item2 > 0)
            {
                _playerId = gameThings.Item1;
                _gameId = gameThings.Item2;
                var division = GetDivisionForCoordinates(xCoords, yCoords);

                var newTile = Repo.GetNewExploredMapTile(_gameId, xCoords, yCoords, _playerId, division);

                return newTile;
            }
            return null;
        }

        public List<Coordinates> GetExploreOptions(string game, string playerName)
        {
            var gameData = Repo.GetPlayerAndGameIds(playerName, game);

            _exploredTiles = Repo.GetExploredTiles(gameData.Item2);

            var playerTiles = _exploredTiles.Where(e => e.PlayerId == gameData.Item1);

            var validTerritories = new List<Coordinates>();

            foreach (var tile in playerTiles)
            {
                validTerritories.AddRange(GetValidTilesForExplore(tile));
            }

            return validTerritories;
        }

        public List<Coordinates> GetValidTilesForExplore(MapTile tile)
        {
            var allNeighbors = GetNeighbors(tile.x ?? 0, tile.y ?? 0);
            var validCoords = new List<Coordinates>();

            //Wormholes first
            for (var i = 0; i < 6; i++)
            {
                if (tile.Wormholes[i] == 1)
                {
                    if (!_exploredTiles.Any(z => z.x == allNeighbors[i].X && z.y == allNeighbors[i].Y))
                    {
                        validCoords.Add(allNeighbors[i]);
                    }
                }
            }

            return validCoords;

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

        public int GetDivisionForCoordinates(int xCoord, int yCoord)
        {
            if (xCoord < 4 || xCoord > 8 || yCoord < 3 || yCoord > 7)
            {
                return 3;
            }

            switch(xCoord) {
    
                case 4:
                case 8:
                    if (yCoord < 4 || yCoord > 6) {
                        return 3;
                    } else {
                        return 2;
                    }
                    break;
                case 5:
                case 7:
                    if (yCoord < 3 || yCoord > 6) {
                        return 3;
                    } else if (yCoord == 3 || yCoord == 6) {
                        return 2;
                    } else {
                        return 1;
                    }
                    break;

                case 6:
                    if (yCoord < 3 || yCoord > 7) {
                        return 3;
                    } else if (yCoord == 3 || yCoord == 7) {
                        return 2;
                    } else if (yCoord == 4 || yCoord == 6) {
                        return 1;
                    } else {
                        return 0;
                    }
                    break;
                default:
                    return 1;
            }

            return 1;
        }
        
    }


    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
