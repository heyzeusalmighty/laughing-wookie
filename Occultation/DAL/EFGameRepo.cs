﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL.EF;
using Occultation.DataModels;


namespace Occultation.DAL
{
    // ReSharper disable once InconsistentNaming
    public class EFGameRepo : IGameRepository
    {

        #region repoMethods

        private GameModel context;

        public EFGameRepo()
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




        public GameBoard GetGameBoard(int gameId)
        {

            return null;
        }

        public int CreateGame()
        {
            var newGame = new Game
            {
                Round = 0,
                GameIdentifier = Guid.NewGuid().ToString(),
                Status = "CREATED"
            };
            context.Games.Add(newGame);
            context.SaveChanges();
            return newGame.GameId;
        }

        public string AddPlayerToGame(int userId, int gameId, string color)
        {
            var user = context.GameUsers.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                var playa = new Player
                {
                    Username = user.UserName,
                    AvailableDiscs = 17,
                    TotalDiscs = 17,
                    BrownIncome = 0,
                    OrangeIncome = 0,
                    PinkIncome = 0,
                    CurrentBrown = 3,
                    CurrentPink = 3,
                    CurrentOrange = 2,
                    DiscColor = color,
                    Pass = false,
                    TurnOrder = 0,
                    UserId = userId,
                    GameId = gameId
                };
                context.Players.Add(playa);
                context.SaveChanges();
                return "Success";
            }

            return "User Not Found";

        }

        public List<Player> GetPlayersForGame(int gameId)
        {
            return context.Players.Where(x => x.GameId == gameId).ToList();
        }

        public Game GetGame(string gameGuid)
        {
            return context.Games.FirstOrDefault(x => x.GameIdentifier == gameGuid);
        }

        public void AddScienceTileToTrack(PlayerTrack track)
        {
            context.PlayerTracks.Add(track);
            context.SaveChanges();
        }

        public void AddNewShipModel(PlayerShipModel model, List<ShipComponent> components)
        {
            context.PlayerShipModels.Add(model);
            context.SaveChanges();

            foreach (var com in components)
            {
                var up = new ShipModelComponent
                {
                    ComponentId = com.ComponentId,
                    ShipId = model.ModelId
                };
                context.ShipModelComponents.Add(up);
            }
            context.SaveChanges();
        }

        public void AddTilesToNewGame(List<MapDeck> tiles)
        {
            foreach (var tile in tiles)
            {
                context.MapDecks.Add(tile);
            }
            context.SaveChanges();
        }

        public List<MapDeck> GetRevealedTiles(int gameId)
        {
            return context.MapDecks.Where(x => x.GameId == gameId && x.Revealed).ToList();
        }

        public MapDeck GetNextTile(int gameId, int div, int x, int y)
        {
            var tile = context.MapDecks.First(c => c.GameId == gameId && c.Division == div && !c.Revealed);
            if (tile == null)
            {
                return null;
            }
            else
            {
                tile.Revealed = true;
                tile.XCoords = x;
                tile.YCoords = y;
                context.SaveChanges();
                return tile;
            }
        }

        public Player GetCurrentUser(int gameId, string userName)
        {
            return null;
        }

        public List<ScienceTile> GetScienceTrack(int gameId, int playerId)
        {
            return null;
        }



    }
}