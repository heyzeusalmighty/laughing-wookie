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
    public class BuildMapViewModel
    {

        private IGameRepository repository;

        public BuildMapViewModel()
        {
            repository = new EFGameRepo();
        }

        public BuildMapViewModel(IGameRepository repo)
        {
            repository = repo;
        }
        
        public string GetLastGame()
        {
            var game = repository.GetLastGame();
            return game.GameIdentifier;
        }

        public GameMap GetGameMap(string gameGuid)
        {
            var map = new GameMap
            {
                MapTiles = GetMapTiles(gameGuid),
                Counts = GetUnrevealedTiles(gameGuid),
                Ships = GetShipsForMap(gameGuid),
                Players = GetPlayersForGame(gameGuid)
            };
            return map;
        }

        private List<MapTile> GetMapTiles(string gameGuid)
        {
            var game = repository.GetGame(gameGuid);
            var revealed = new List<MapTile>();
            if (game != null)
            {
                
                var realTiles = new AvailableMapTile().AllTheTiles;
                var tiles = repository.GetRevealedTiles(game.GameId);
                foreach (var tiley in tiles)
                {
                    var realTile = realTiles.FirstOrDefault(x => x.MapId == tiley.MapId);
                    if (realTile != null)
                    {
                        var holes = new int[6];
                        if (tiley.WormHoleIndex != null)
                        {
                            var idx = 0;
                            var tId = tiley.WormHoleIndex ?? 0;


                            for (var i = tId; i < 6; i++)
                            {
                                holes[idx] = realTile.Wormholes[i];
                                idx++;
                            }

                            for (var x = 0; x < tId; x++)
                            {
                                holes[idx] = realTile.Wormholes[x];
                                idx++;
                            }
                        }
                        else
                        {
                            holes = realTile.Wormholes;
                        }



                        var occupied = new MapTile
                        {
                            Aliens = realTile.Aliens,
                            Brown = realTile.Brown,
                            BrownAdvanced = realTile.BrownAdvanced,
                            Division = realTile.Division,
                            MapId = realTile.MapId,
                            Occupied = tiley.Occupied,
                            Orange = realTile.Orange,
                            OrangeAdvanced = realTile.OrangeAdvanced,
                            Pink = realTile.Pink,
                            PinkAdvanced = realTile.PinkAdvanced,
                            VictoryPoints = realTile.VictoryPoints,
                            White = realTile.White,
                            Wormholes = holes,
                            x = tiley.XCoords,
                            y = tiley.YCoords,
                            IsSet = (tiley.WormHoleIndex != null),
                            PlayerId = tiley.PlayerId
                        };
                        revealed.Add(occupied);

                    }
                }
            }
            return revealed;
        }

        private RemainingMapCounts GetUnrevealedTiles(string gameGuid)
        {
            var game = repository.GetGame(gameGuid);
            var counts = new RemainingMapCounts {GameId = gameGuid};

            if (game != null)
            {
                var realTiles = new AvailableMapTile().AllTheTiles;
                var tiles = repository.GetRevealedTiles(game.GameId);
                foreach (var tiley in tiles)
                {
                    var tileToBeRemoved = realTiles.FirstOrDefault(x => x.MapId == tiley.MapId);
                    if (tileToBeRemoved != null)
                    {
                        realTiles.Remove(tileToBeRemoved);
                    }
                }

                //revealed.AddRange(realTiles.Select(remaining => new MapTile {Division = remaining.Division}));

                foreach (var remains in realTiles)
                {
                    switch (remains.Division)
                    {
                        case 1:
                            counts.DivisionOne++;
                            break;
                        case 2:
                            counts.DivisionTwo++;
                            break;
                        case 3:
                            counts.DivisionThree++;
                            break;
                        default:
                            break;
                    }
                }
            }
            return counts;
        }

        private List<ShipForMap> GetShipsForMap(string gameGuid)
        {
            return repository.GetShipsForGame(gameGuid);
        }

        private List<Player> GetPlayersForGame(string gameGuid)
        {
            var game = repository.GetGame(gameGuid);
            var fullPlayers = repository.GetPlayersForGame(game.GameId);
            var simpleList = (from player in fullPlayers
                select new Player
                {
                    Username = player.Username,
                    DiscColor = player.DiscColor,
                    CurrentBrown = player.CurrentBrown,
                    CurrentOrange = player.CurrentOrange,
                    CurrentPink = player.CurrentPink,
                    TurnOrder = player.TurnOrder,
                    Pass = player.Pass,
                    PlayerId = player.PlayerId
                }
                );
            return simpleList.ToList();
        }
    }
}
