﻿using System;
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

        public ExploratoryPackage ExploreTile(string name, string game, int xCoords, int yCoords)
        {
            var gameThings = Repo.GetPlayerAndGameIds(name, game);

            if (gameThings.Item1 > 0 && gameThings.Item2 > 0)
            {
                _playerId = gameThings.Item1;
                _gameId = gameThings.Item2;
                var division = GetDivisionForCoordinates(xCoords, yCoords);


                var package = new ExploratoryPackage();
                var tile = Repo.GetNewExploredMapTile(_gameId, xCoords, yCoords, _playerId, division);
                //var reward = Repo.GetDiscoveryTile(_gameId, _playerId);

                if (tile == null)
                {
                    package.Message = "There are no more tiles to discover in this sector";
                    return package;
                }



                package.Message = "Success";
                package.Tile = tile;
                //package.Reward = reward;
                return package;
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
                        if (allNeighbors[i].X > -1 && allNeighbors[i].Y > -1)
                        {
                            validCoords.Add(allNeighbors[i]);
                        }
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
                    new Coordinates {X = x, Y = y - 1},
                    new Coordinates {X = x + 1, Y = y - 1},
                    new Coordinates {X = x + 1, Y = y},
                    new Coordinates {X = x, Y = y + 1},
                    new Coordinates {X = x - 1, Y = y},
                    new Coordinates {X = x - 1, Y = y - 1}
                };
            }

            return new Coordinates[]
            {
                new Coordinates {X = x, Y = y - 1},
                new Coordinates {X = x + 1, Y = y},
                new Coordinates {X = x + 1, Y = y + 1},
                new Coordinates {X = x, Y = y + 1},
                new Coordinates {X = x - 1, Y = y + 1},
                new Coordinates {X = x - 1, Y = y}
            };


        }

        public int GetDivisionForCoordinates(int xCoord, int yCoord)
        {
            if (xCoord < 4 || xCoord > 8 || yCoord < 3 || yCoord > 7)
            {
                return 3;
            }

            switch (xCoord)
            {

                case 4:
                case 8:
                    if (yCoord < 4 || yCoord > 6)
                    {
                        return 3;
                    }
                    else
                    {
                        return 2;
                    }
                    break;
                case 5:
                case 7:
                    if (yCoord < 3 || yCoord > 6)
                    {
                        return 3;
                    }
                    else if (yCoord == 3 || yCoord == 6)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                    break;

                case 6:
                    if (yCoord < 3 || yCoord > 7)
                    {
                        return 3;
                    }
                    else if (yCoord == 3 || yCoord == 7)
                    {
                        return 2;
                    }
                    else if (yCoord == 4 || yCoord == 6)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                default:
                    return 1;
            }

            return 1;
        }

        public string SetWormholeIndex(int map, int index, string game, string player)
        {
            var gameData = Repo.GetPlayerAndGameIds(player, game);
            if (gameData.Item1 > 0 && gameData.Item2 > 0)
            {
                return Repo.SetWormHoleIndex(map, index, gameData.Item2, gameData.Item1);
            }
            return "Game or User not found";

        }

        public ExploratoryPackage InfluenceTileFromExplore(int map, string game, string player, bool answer)
        {
            var gameData = Repo.GetPlayerAndGameIds(player, game);
            if (gameData.Item1 > 0 && gameData.Item2 > 0)
            {
                _gameId = gameData.Item2;
                _playerId = gameData.Item1;
                if (answer)
                {
                    var tile = Repo.InfluenceTile(map, _gameId, _playerId);
                    Repo.DecrementDiscFromPlayer(_playerId);


                    var package = new ExploratoryPackage();

                    var allTheTiles = new AvailableMapTile().AllTheTiles;
                    var actualTile = allTheTiles.FirstOrDefault(x => x.MapId == map);
                    if (actualTile != null)
                    {
                        if (actualTile.Reward)
                        {
                            var reward = Repo.ClaimDiscoveryTile(_gameId, _playerId, tile);
                            if (reward == null)
                            {
                                package.Message = "There are no more rewards for discovery";
                                return package;
                            }

                            package.Message = "Influenced";
                            package.Reward = reward;
                            return package;
                        }
                        else
                        {
                            package.Message = "No Reward";
                            return package;
                        }
                    }

                    package.Message = "Tile not found for some reason";
                    return package;

                }
                else
                {
                    var tiel = Repo.RemoveInfluence(map, gameData.Item2, gameData.Item1);
                    return new ExploratoryPackage {Message = "Not influencing"};
                }


            }

            return new ExploratoryPackage {Message = "Game or User not found"};
        }

        public string InfluenceTile(int map, string game, string player, bool answer)
        {
            var gameData = Repo.GetPlayerAndGameIds(player, game);
            if (gameData.Item1 > 0 && gameData.Item2 > 0)
            {
                if (Repo.InfluenceTile(map, gameData.Item2, gameData.Item1) >= 0)
                {
                    return "Success";
                }
            }

            return "Game or User not found";

        }


        public class Coordinates
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }

}