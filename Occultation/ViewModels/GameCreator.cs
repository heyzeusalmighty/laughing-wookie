using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.ViewModels
{
    public class GameCreator
    {
        private IGameRepository Repository;
        private List<Player> Players;
        private List<MapTile> PlayerTiles;
        private AvailableMapTile TileBuilder;
        public GameCreator(IGameRepository repo)
        {
            Repository = repo;
            TileBuilder = new AvailableMapTile();
            PlayerTiles = TileBuilder.PlayerTiles();
        }

        
        public string AddPlayerToGame(int userId, string gameGuid, string color)
        {
            var game = Repository.GetGame(gameGuid);
            if (game != null)
            {
                Players = Repository.GetPlayersForGame(game.GameId);
                if (Players.Any(x => x.UserId == userId))
                {
                    return "Cannot play twice in one game";
                }

                //if (Players.Any(x => x.DiscColor == color))
                //{
                //    return "Please select a different color";
                //}

                var msg = Repository.AddPlayerToGame(userId, game.GameId);

                if (Players.Count == 5)
                {
                    StartGame(gameGuid);
                }

                return msg;

            }
            return "Game Not Found";
        }


        public void StartGame(string gameGuid)
        {
            var game = Repository.GetGame(gameGuid);
            if (game != null)
            {
                Players = Repository.GetPlayersForGame(game.GameId);


                foreach (var playa in Players)
                {
                    //build player science tracks
                    BuildScienceTrack(playa.PlayerId);

                    //build ships
                    BuildShip(playa.PlayerId);
                }

                

                //build map tiles
                BuildMapTiles(game.GameId);
                //arrange player tiles
                //place ships

            }
        }



        public void BuildScienceTrack(int playerId)
        {
            var spacePort = new PlayerTrack
            {
                PlayerId = playerId,
                Position = 1,
                Track = "Star",
                TileId = 1
            };
            Repository.AddScienceTileToTrack(spacePort);

        }


        public void BuildShip(int playerId)
        {

            var interceptor = new Interceptor();
            var model = new PlayerShipModel
            {
                ModelName = "interceptor",
                PlayerId = playerId
            };

            Repository.AddNewShipModel(model, interceptor.Components);

        }

        public void BuildMapTiles(int gameId)
        {

            
            var gameMap = new List<MapDeck>();

            var counter = 1;
            foreach (var tile1 in TileBuilder.DivisionOne)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 1,
                    GameId = gameId,
                    MapId = tile1.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = tile1.Occupied
                });
                counter++;
            }
            //Player Bases
            var playerTiles = BuildPlayerBases(gameId);
            SavePlayerPositions(playerTiles);
            gameMap.AddRange(playerTiles);

            counter = 1;
            foreach (var tile2 in TileBuilder.DivisionTwo)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = tile2.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = tile2.Occupied
                });
                counter++;
            }


            counter = 1;
            foreach (var tile3 in TileBuilder.DivisionThree)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 3,
                    GameId = gameId,
                    MapId = tile3.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = tile3.Occupied
                });
                counter++;
            }

            // Central Tile
            var central = TileBuilder.GetCentralTile();
            gameMap.Add(new MapDeck
            {
                Division = 0,
                GameId = gameId,
                MapId = central.MapId,
                SortOrder = 0,
                Revealed = true,
                Occupied = central.Occupied,
                XCoords = 6,
                YCoords = 5
            });


            Repository.AddTilesToNewGame(gameMap);

        }

        public List<MapDeck> BuildPlayerBases(int gameId)
        {
            var bases = new List<MapDeck>();

            var playerCount = Players.Count();

            switch (playerCount)
            {
                case 2:
                    bases.Add(BuildBlackPlayer(gameId));
                    bases.Add(BuildRedPlayer(gameId));
                    break;
                case 3:
                    bases.Add(BuildYellowPlayer(gameId));
                    bases.Add(BuildBlackPlayer(gameId));
                    bases.Add(BuildBluePlayer(gameId));
                    break;
                case 4:
                    bases.Add(BuildWhitePlayer(gameId));
                    bases.Add(BuildYellowPlayer(gameId));
                    bases.Add(BuildGreenPlayer(gameId));
                    bases.Add(BuildBluePlayer(gameId));
                    break;
                case 5:
                    bases.Add(BuildWhitePlayer(gameId));
                    bases.Add(BuildYellowPlayer(gameId));
                    bases.Add(BuildGreenPlayer(gameId));
                    bases.Add(BuildBluePlayer(gameId));
                    bases.Add(BuildBlackPlayer(gameId));
                    break;
                case 6:
                    bases.Add(BuildWhitePlayer(gameId));
                    bases.Add(BuildYellowPlayer(gameId));
                    bases.Add(BuildGreenPlayer(gameId));
                    bases.Add(BuildBluePlayer(gameId));
                    bases.Add(BuildBlackPlayer(gameId));
                    bases.Add(BuildRedPlayer(gameId));
                    break;
            }

            return bases;
        }

        private MapDeck BuildBluePlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("Blue"));
            var coords = GetCoordsForColor("Blue");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "Blue"
                };
            }
            return null;
        }

        private MapDeck BuildRedPlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("Red"));
            var coords = GetCoordsForColor("Red");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "Red"
                };
            }
            return null;
        }

        private MapDeck BuildGreenPlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("Green"));
            var coords = GetCoordsForColor("Green");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "Green"
                };
            }
            return null;
        }

        private MapDeck BuildYellowPlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("Yellow"));
            var coords = GetCoordsForColor("Yellow");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "Yellow"
                };
            }
            return null;
        }

        private MapDeck BuildWhitePlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("White"));
            var coords = GetCoordsForColor("White");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "White"
                };
            }
            return null;
        }

        private MapDeck BuildBlackPlayer(int gameId)
        {
            var black = PlayerTiles.FirstOrDefault(x => x.Occupied.Equals("Black"));
            var coords = GetCoordsForColor("Black");
            if (black != null)
            {
                return new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = black.MapId,
                    SortOrder = 0,
                    Revealed = true,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2,
                    Occupied = "Black"
                };
            }
            return null;
        }

        private void SavePlayerPositions(List<MapDeck> tiles)
        {
            var playerList = Players.ToArray();

            var count = 0;
            foreach (var tile in tiles)
            {
                Repository.SetPlayerColor(playerList[count].PlayerId, tile.Occupied);
                count++;
            }
            Repository.Save();
        }

        private static Tuple<int, int> GetCoordsForColor(string color)
        {
            switch (color)
            {
                case "Black":
                    return new Tuple<int, int>(6, 3);
                    break;
                case "Green":
                    return new Tuple<int, int>(8, 4);
                    break;
                case "Blue":
                    return new Tuple<int, int>(8, 6);
                    break;
                case "Red":
                    return new Tuple<int, int>(6, 7);
                    break;
                case "Yellow":
                    return new Tuple<int, int>(4, 6);
                    break;
                case "White":
                    return new Tuple<int, int>(4, 4);
                    break;
                default:
                    return null;

            }
        }
    }
}
