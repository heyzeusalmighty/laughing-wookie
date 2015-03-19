using System;
using System.Collections.Generic;
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
        public GameCreator(IGameRepository repo)
        {
            Repository = repo;
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

                if (Players.Any(x => x.DiscColor == color))
                {
                    return "Please select a different color";
                }

                var msg = Repository.AddPlayerToGame(userId, game.GameId, color);

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

            var tiles = new AvailableMapTile();
            var gameMap = new List<MapDeck>();

            var counter = 1;
            foreach (var tile1 in tiles.DivisionOne)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 1,
                    GameId = gameId,
                    MapId = tile1.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = (tile1.Aliens > 0) ? "Aliens" : ""
                });
                counter++;
            }

            gameMap.AddRange(BuildPlayerBases(gameId));
            counter = 1;
            foreach (var tile2 in tiles.DivisionTwo)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = tile2.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = (tile2.Aliens > 0) ? "Aliens" : ""
                });
                counter++;
            }


            counter = 1;
            foreach (var tile3 in tiles.DivisionThree)
            {
                gameMap.Add(new MapDeck
                {
                    Division = 3,
                    GameId = gameId,
                    MapId = tile3.MapId,
                    SortOrder = counter,
                    Revealed = false,
                    Occupied = (tile3.Aliens > 0) ? "Aliens" : ""
                });
                counter++;
            }

            Repository.AddTilesToNewGame(gameMap);

        }

        public List<MapDeck> BuildPlayerBases(int gameId)
        {
            var bases = new List<MapDeck>();
            foreach (var playa in Players)
            {
                var coords = GetCoordsForColor(playa.DiscColor);
                bases.Add(new MapDeck
                {
                    Division = 2,
                    GameId = gameId,
                    MapId = 666,
                    SortOrder = 0,
                    Revealed = true,
                    Occupied = playa.DiscColor,
                    XCoords = coords.Item1,
                    YCoords = coords.Item2
                });
            }
            return bases;
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
