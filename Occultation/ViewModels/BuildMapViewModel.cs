using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL;
using Occultation.DataModels;

namespace Occultation.ViewModels
{
    public class BuildMapViewModel
    {

        private IGameRepository repository;
        public BuildMapViewModel(IGameRepository repo)
        {
            repository = repo;
        }

        public List<MapTile> GetGameMap(string gameGuid)
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
                            Wormholes = realTile.Wormholes,
                            x = tiley.XCoords,
                            y = tiley.YCoords
                        };
                        revealed.Add(occupied);

                    }
                }
            }
            return revealed;
        }
    }
}
