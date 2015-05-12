using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.DAL
{
    public interface ITurnRepository : IDisposable
    {
        Tuple<int, int> GetPlayerAndGameIds(string name, string gameGuid);
        MapTile GetNewExploredMapTile(int gameId, int xCoords, int yCoords, int playerId);
    }

    public class TurnRepository : ITurnRepository
    {
        
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
                        context.Players.FirstOrDefault(x => x.GameId == game.GameId && x.PlayerId == gamePlayer.UserId);
                    return (player != null)
                        ? new Tuple<int, int>(player.PlayerId, game.GameId)
                        : new Tuple<int, int>(-1, -1);
                }
                return new Tuple<int, int>(-1, -1);
            }
            return new Tuple<int, int>(-1, -1);
        }

        public MapTile GetNewExploredMapTile(int gameId, int xCoords, int yCoords, int playerId)
        {
            var firstAvailable =
                context.MapDecks.Where(x => x.GameId == gameId && x.XCoords == null && x.YCoords == null).OrderBy(x => x.SortOrder);
            


            return null;
        }

        #endregion

    }

}
