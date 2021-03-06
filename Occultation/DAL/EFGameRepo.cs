﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using NLog;
using Occultation.DAL.EF;
using Occultation.DataModels;


namespace Occultation.DAL
{
    // ReSharper disable once InconsistentNaming
    public class EFGameRepo : IGameRepository
    {

        //private Logger _logger = new 

        

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

        public int CreateGame(string title)
        {
            var newGame = new Game
            {
                Round = 0,
                GameIdentifier = Guid.NewGuid().ToString(),
                Status = "CREATED",
                Title = title
            };
            context.Games.Add(newGame);
            context.SaveChanges();
            return newGame.GameId;
        }

        public string AddPlayerToGame(int userId, int gameId)
        {
            var user = context.GameUsers.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                var playerCount = context.Players.Count(x => x.GameId == gameId);
                var playa = new Player
                {
                    Username = user.UserName,
                    AvailableDiscs = 13,
                    TotalDiscs = 17,
                    BrownIncome = 0,
                    OrangeIncome = 0,
                    PinkIncome = 0,
                    CurrentBrown = 3,
                    CurrentPink = 3,
                    CurrentOrange = 2,
                    DiscColor = "Super",
                    AvailableColonyShips = 3,
                    Pass = false,
                    TurnOrder = playerCount + 1,
                    UserId = userId,
                    GameId = gameId
                };
                context.Players.Add(playa);
                //context.SaveChanges();
                Save();
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

        public Game GetGame(int gameId)
        {
            return context.Games.FirstOrDefault(x => x.GameId == gameId);
        }

        public Game GetLastGame()
        {
            return context.Games.OrderByDescending(x => x.GameId).First();
        }

        public void AddScienceTileToTrack(PlayerTrack track)
        {
            context.PlayerTracks.Add(track);
            context.SaveChanges();
        }

        public PlayerShipModel AddNewShipModel(PlayerShipModel model, List<ShipComponent> components)
        {
            context.PlayerShipModels.Add(model);
            context.SaveChanges();

            foreach (var com in components)
            {
                var up = new ShipModelComponent
                {
                    ComponentId = com.ComponentId,
                    ShipId = model.ModelId,
                    ComponentName = com.Name
                };
                context.ShipModelComponents.Add(up);
            }
            context.SaveChanges();
            return model;
        }

        public void AddTilesToNewGame(List<MapDeck> tiles)
        {
            context.MapDecks.AddRange(tiles);
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

        public void SetPlayerColor(int playerId, string color, int mapDeckId)
        {

            var user = context.Players.FirstOrDefault(x => x.PlayerId == playerId);
            if (user != null)
            {
                user.DiscColor = color;
            }

            var tile = context.MapDecks.FirstOrDefault(x => x.MapDeckId == mapDeckId);
            if (tile != null)
            {
                tile.PlayerId = playerId;
            }

        }

        public List<GameUser> GetAllGameUsers()
        {
            return context.GameUsers.ToList();
        }

        public List<Game> GetAllGames()
        {
            return context.Games.ToList();
        }

        public void SaveNewShip(PlayerShip ship)
        {
            context.PlayerShips.Add(ship);
        }

        public List<ShipForMap> GetShipsForGame(string gameGuid)
        {
            var game = GetGame(gameGuid);
            
            var playerShips = (from ship in context.PlayerShips
                               join model in context.PlayerShipModels on ship.ModelId equals model.ModelId
                               join players in context.Players on ship.PlayerId equals players.PlayerId
                               //join comp in context.ShipModelComponents on model.ModelId equals comp.ShipId 
                               where players.GameId == game.GameId

                               select new ShipForMap
                               {
                                   Color = players.DiscColor,
                                   ShipId = ship.PlayerShipId,
                                   XCoords = ship.XCoords,
                                   YCoords = ship.YCoords,
                                   ShipType = model.ModelName,
                               }).ToList();

            foreach (var ship in playerShips)
            {
                ship.Components = context.ShipModelComponents.Where(x => x.ShipId == ship.ShipId).ToList();

                if (ship.ShipType == "interceptor")
                {
                    ship.Stats = new Interceptor(ship.Components);
                }
            }

            return playerShips;

        }
		

        public Player GetCurrentUser(int gameId, string userName)
        {
            return context.Players.FirstOrDefault(x => x.GameId == gameId && x.Username.Equals(userName));
        }

        public List<ScienceTile> GetScienceTrack(int gameId, int playerId)
        {
            return null;
        }

        public void SetGameStatus(string status, int gameId)
        {
            var game = context.Games.FirstOrDefault(x => x.GameId == gameId);
            if (game != null)
            {
                game.Status = status;
                context.SaveChanges();
            }
        }

        public void SetDiscoveryTilesForGame(List<GameDiscovery> tiles)
        {
            context.GameDiscoveries.AddRange(tiles);
        }

        public void SetFirstPlayerTurn(int gameId)
        {
            var player = context.Players.FirstOrDefault(x => x.GameId == gameId && x.TurnOrder == 1);
            if (player != null)
            {
                player.WhosTurn = true;
                context.SaveChanges();
            }
            
        }
    }
}
