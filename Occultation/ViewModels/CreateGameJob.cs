﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.ViewModels
{
    public class StartGameJob
    {
        private IGameRepository Repository;
        public List<Player> Players;
        public List<MapTile> PlayerTiles;
        public AvailableMapTile TileBuilder;
        public List<MapDeck> PlayerDeck;
        private List<PlayerShipModel> shipModels;
        public List<DiscoveryTile> DiscoveryTiles;

        public StartGameJob(IGameRepository repo)
        {
            Repository = repo;
            TileBuilder = new AvailableMapTile();
            PlayerTiles = TileBuilder.PlayerTiles();
        }


        public void StartGame(string gameGuid)
        {
            var game = Repository.GetGame(gameGuid);
            if (game != null)
            {
                if (game.Status == "CREATED")
                {
                    Players = Repository.GetPlayersForGame(game.GameId);

                    //build map tiles
                    BuildMapTiles(game.GameId);
                    BuildDiscoveryTiles(game.GameId);
                    Repository.Save();

                    foreach (var playa in Players)
                    {
                        //build player science tracks
                        BuildScienceTrack(playa.PlayerId);

                        //build ships
                        BuildShip(playa.PlayerId);
                    }

                    Repository.SetGameStatus("STARTED", game.GameId);
                    SetFirstPlayerTurn(game.GameId);
                }
            }
        }

        private void BuildMapTiles(int gameId)
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
            BuildPlayerBases(gameId);
            gameMap.AddRange(PlayerDeck);

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
            SavePlayerPositions();
        }

        public void BuildPlayerBases(int gameId)
        {
            PlayerDeck = new List<MapDeck>();

            var playerCount = Players.Count();

            switch (playerCount)
            {
                case 2:
                    PlayerDeck.Add(BuildBlackPlayer(gameId));
                    PlayerDeck.Add(BuildRedPlayer(gameId));
                    break;
                case 3:
                    PlayerDeck.Add(BuildYellowPlayer(gameId));
                    PlayerDeck.Add(BuildBlackPlayer(gameId));
                    PlayerDeck.Add(BuildBluePlayer(gameId));
                    break;
                case 4:
                    PlayerDeck.Add(BuildWhitePlayer(gameId));
                    PlayerDeck.Add(BuildYellowPlayer(gameId));
                    PlayerDeck.Add(BuildGreenPlayer(gameId));
                    PlayerDeck.Add(BuildBluePlayer(gameId));
                    break;
                case 5:
                    PlayerDeck.Add(BuildWhitePlayer(gameId));
                    PlayerDeck.Add(BuildYellowPlayer(gameId));
                    PlayerDeck.Add(BuildGreenPlayer(gameId));
                    PlayerDeck.Add(BuildBluePlayer(gameId));
                    PlayerDeck.Add(BuildBlackPlayer(gameId));
                    break;
                case 6:
                    PlayerDeck.Add(BuildWhitePlayer(gameId));
                    PlayerDeck.Add(BuildYellowPlayer(gameId));
                    PlayerDeck.Add(BuildGreenPlayer(gameId));
                    PlayerDeck.Add(BuildBluePlayer(gameId));
                    PlayerDeck.Add(BuildBlackPlayer(gameId));
                    PlayerDeck.Add(BuildRedPlayer(gameId));
                    break;
            }

            var playerIds = Players.Select(x => x.PlayerId).ToArray();
            var counter = 0;
            foreach(var deck in PlayerDeck)
            {
                deck.PlayerId = playerIds[counter];
                counter++;
            }



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

        private void SavePlayerPositions()
        {
            var playerList = Players.ToArray();

            var count = 0;
            foreach (var tile in PlayerDeck)
            {
                Repository.SetPlayerColor(playerList[count].PlayerId, tile.Occupied, tile.MapDeckId);
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
            if (shipModels == null)
            {
                shipModels = new List<PlayerShipModel>();
            }

            var interceptor = new Interceptor();
            var model = new PlayerShipModel
            {
                ModelName = "interceptor",
                PlayerId = playerId
            };

            var shipModel = Repository.AddNewShipModel(model, interceptor.Components);

            shipModels.Add(shipModel);

            
            var playerTile = PlayerDeck.FirstOrDefault(x => x.PlayerId == playerId);
            if (playerTile != null)
            {
                var firstShip = new PlayerShip
                {
                    ModelId = shipModel.ModelId,
                    PlayerId = playerId,
                    XCoords = playerTile.XCoords,
                    YCoords = playerTile.YCoords
                };

                var secondShip = new PlayerShip
                {
                    ModelId = shipModel.ModelId,
                    PlayerId = playerId,
                    XCoords = playerTile.XCoords,
                    YCoords = playerTile.YCoords
                };

                Repository.SaveNewShip(firstShip);
                Repository.SaveNewShip(secondShip);
            }

            Repository.Save();

        }

        public void BuildDiscoveryTiles(int gameId)
        {
            DiscoveryTiles = new AllDiscoveryTiles().Tiles;
            
            var tileList = new List<GameDiscovery>();
            var counter = 1;
            foreach (var tile in DiscoveryTiles)
            {
                tileList.Add(new GameDiscovery
                {
                    DiscoveryId = tile.Id, 
                    Revealed = false, 
                    SortOrder = counter,
                    GameId = gameId,
                    Claimed = null
                });
                counter++;
            }
            Repository.SetDiscoveryTilesForGame(tileList);
        }

        public void SetFirstPlayerTurn(int gameId)
        {
            Repository.SetFirstPlayerTurn(gameId);
        }

        
    }
}
