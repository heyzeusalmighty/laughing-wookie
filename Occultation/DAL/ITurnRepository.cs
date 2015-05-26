using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.DAL
{
    public interface ITurnRepository : IDisposable
    {
        Tuple<int, int> GetPlayerAndGameIds(string name, string gameGuid);
        MapTile GetNewExploredMapTile(int gameId, int xCoords, int yCoords, int playerId,int division);
        int[] SetWormHoles(int index, int[] wormholes);
        List<MapTile> GetExploredTiles(int gameId);
    }

    public class TurnRepository : ITurnRepository
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        #region repoMethods

        private GameModel context;

        public TurnRepository()
        {
            context = new GameModel();
        }
        public void Save()
        {
            //context.SaveChanges();
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var returnstring = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {

                    var errorRR = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        var more = String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        errorRR += more;
                    }
                    returnstring.Add(errorRR);
                }
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion

        #region methods

        public Tuple<int, int> GetPlayerAndGameIds(string name, string gameGuid)
        {
            var gamePlayer = context.GameUsers.FirstOrDefault(x => x.UserName == name);
            if (gamePlayer != null)
            {
                var game = context.Games.FirstOrDefault(x => x.GameIdentifier == gameGuid);
                if (game != null)
                {
                    var player =
                        context.Players.FirstOrDefault(x => x.GameId == game.GameId && x.UserId == gamePlayer.UserId);
                    return (player != null)
                        ? new Tuple<int, int>(player.PlayerId, game.GameId)
                        : new Tuple<int, int>(-1, -1);
                }
                _logger.Info("{0} called {1} doesnt belong to a game", name, gameGuid);
                return new Tuple<int, int>(-1, -1);
            }
            _logger.Info("{0} isn't a game player", name);
            return new Tuple<int, int>(-1, -1);
        }

        public MapTile GetNewExploredMapTile(int gameId, int xCoords, int yCoords, int playerId, int division)
        {
            var firstAvailable =
                context.MapDecks.Where(x => x.GameId == gameId && x.XCoords == null && x.YCoords == null && x.Division == division).OrderBy(x => x.SortOrder);
            if (firstAvailable.Any())
            {

                if (context.MapDecks.Any(x => x.XCoords == xCoords && x.YCoords == yCoords && x.GameId == gameId))
                {
                    _logger.Info("{0} in Game {1} attempted to place tile on occupied spot", playerId, gameId);
                    return null;
                }

                var discovered = firstAvailable.First();
                discovered.XCoords = xCoords;
                discovered.YCoords = yCoords;
                discovered.Revealed = true;
                discovered.PlayerId = playerId;
                context.SaveChanges();

                var tiles = new AvailableMapTile();
                var yay = tiles.AllTheTiles.FirstOrDefault(x => x.MapId == discovered.MapId);
                if (yay != null)
                {
                    var player = context.Players.FirstOrDefault(x => x.PlayerId == playerId);

                    yay.Occupied = (player != null) ? player.DiscColor : "unknown";
                    yay.x = xCoords;
                    yay.y = yCoords;

                    _logger.Info("{0} successfully explored tile {1} in Game {2}", playerId, yay.MapId, gameId);
                    return yay;
                }
            }
            _logger.Info("{0} in Game {1} tried to explore where all tiles have been discovered", playerId, gameId);
            
            return null;
        }

        public List<MapTile> GetExploredTiles(int gameId)
        {
            var mapdeck = context.MapDecks.Where(x => x.Revealed && x.WormHoleIndex != null).ToList();
            var allZeTiles = new AvailableMapTile();

            var tiles = (from deck in mapdeck
                         join zTiles in allZeTiles.AllTheTiles on deck.MapId equals zTiles.MapId 
                select new MapTile()
                {
                    x = deck.XCoords,
                    y = deck.YCoords,
                    Wormholes = SetWormHoles(deck.WormHoleIndex ?? 0, zTiles.Wormholes),
                    MapId = zTiles.MapId,
                    PlayerId = deck.PlayerId
                }
                );



            return tiles.ToList();
        }



        public int[] SetWormHoles(int index, int[] wormholes)
        {
            if (index == 0)
            {
                return wormholes;
            }

            var holes = new int[6];

            var idx = 0;
            for (int i = index; i < 6; i++)
            {
                holes[idx] = wormholes[i];
                idx++;
            }

            for (int x = 0; x < index; x++)
            {
                holes[idx] = wormholes[x];
                idx++;
            }

            return holes;

        }
        #endregion

    }

}
