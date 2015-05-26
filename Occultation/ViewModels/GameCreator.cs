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

        private List<PlayerShipModel> shipModels;
        public GameCreator(IGameRepository repo)
        {
            Repository = repo;
            TileBuilder = new AvailableMapTile();
            PlayerTiles = TileBuilder.PlayerTiles();
        }

        
        public string AddPlayerToGame(int userId, string gameGuid)
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
                    var start = new StartGameJob(Repository);
                    start.StartGame(gameGuid);
                    //StartGame(gameGuid);
                }

                return msg;

            }
            return "Game Not Found";
        }


    }
}
