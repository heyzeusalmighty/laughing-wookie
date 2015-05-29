
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.DAL
{
    public class FakeGameRepo : IGameRepository
    {
        
         #region repoMethods

        private GameModel context;

        public FakeGameRepo()
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

        public List<Player> AddedPlayers { get; set; }
        public List<PlayerTrack> Tracks { get; set; }
        public List<PlayerShipModel> ShipModel { get; set; }
        public List<ShipModelComponent> ShipComponents { get; set; }
        public List<MapDeck> GameMapTiles { get; set; }
        public List<PlayerShip> PlayerShips { get; set; }
        public List<GameDiscovery> Discoveries { get; set; }
        
        
        public GameBoard GetGameBoard(int gameId)
        {
            switch (gameId)
            {
                case 1:
                    return GetFourPlayerBoard();
                    break;
                default:
                    return null;

            }
        }

        public Player GetCurrentUser(int gameId, string userName)
        {
            return new Player
            {
                UserId = 1, DiscColor = "Green", CurrentBrown = 2, 
                CurrentOrange = 5,
                CurrentPink = 10,
                AvailableDiscs = 5,
                AvailableColonyShips = 3

            };
        }

        public List<ScienceTile> GetScienceTrack(int gameId, int playerId)
        {
            var list =  new List<ScienceTile>();
            list.Add(new ScienceTile
            {
                Image = "../../Content/Images/blankScienceTile.svg", 
                Name = "Star Base", 
                Track = Track.Star, 
                Position = 1
            });
            list.Add(new ScienceTile
            {
                Image = "../../Content/Images/blankScienceTile.svg", 
                Name = "Star Base", 
                Track = Track.Gear, 
                Position = 2
            });
            
            
            
            return list;
        }

        public int CreateGame(string title)
        {
            return 0;
        }

        public string AddPlayerToGame(int userId, int gameId)
        {
            AddedPlayers = new List<Player>
            {
                new Player { UserId = userId, GameId = gameId}
            };
            return "success";
        }

        public List<Player> GetPlayersForGame(int gameId)
        {
            return new List<Player>
            {
                new Player { UserId = 1, DiscColor = "Green"},
                new Player { UserId = 2, DiscColor = "Black"},
                new Player { UserId = 3, DiscColor = "Yellow"},
                new Player { UserId = 4, DiscColor = "White"},
                new Player { UserId = 5, DiscColor = "Blue"}
            };

        }

        public Game GetGame(string gameGuid)
        {
            if (gameGuid == "fakeGuid")
            {
                return null;
            }

            return new Game
            {
                GameId = 45,
                Round = 0,
                Status = "CREATED"
            };
        }

        public Game GetGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Game GetLastGame()
        {
            throw new NotImplementedException();
        }

        public void AddScienceTileToTrack(PlayerTrack track)
        {
            if (Tracks == null)
            {
              Tracks  = new List<PlayerTrack>
                {
                    track
                };
            }
            else
            {
                Tracks.Add(track);
            }
        }

        PlayerShipModel IGameRepository.AddNewShipModel(PlayerShipModel model, List<ShipComponent> components)
        {
            model.ModelId = 4;
            return model;
        }

        public void AddNewShipModel(PlayerShipModel model, List<ShipComponent> components)
        {
            if (ShipModel == null)
            {
                ShipModel = new List<PlayerShipModel>
                {
                    model
                };
                ShipComponents = new List<ShipModelComponent>();
                foreach (var comp in components)
                {
                    ShipComponents.Add(new ShipModelComponent { ComponentId = comp.ComponentId});
                }


            }
            else
            {
                ShipModel.Add(model);
                foreach (var comp in components)
                {
                    ShipComponents.Add(new ShipModelComponent { ComponentId = comp.ComponentId });
                }
            }
        }

        public void AddTilesToNewGame(List<MapDeck> tiles)
        {
            if(GameMapTiles == null)
                GameMapTiles = new List<MapDeck>();

            foreach (var tile in tiles)
            {
                GameMapTiles.Add(tile);
            }
        }

        public List<MapDeck> GetRevealedTiles(int gameId)
        {
            throw new NotImplementedException();
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
            if (AddedPlayers == null)
            {
                AddedPlayers = new List<Player>();
            }
            AddedPlayers.Add(new Player { PlayerId = playerId, DiscColor = color});
        }

        public List<GameUser> GetAllGameUsers()
        {
            throw new NotImplementedException();
        }

        public List<Game> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public void SaveNewShip(PlayerShip ship)
        {
            if (PlayerShips == null)
            {
                PlayerShips = new List<PlayerShip>();
            }

            PlayerShips.Add(ship);
        }

        public List<ShipForMap> GetShipsForGame(string gameGuid)
        {
            throw new NotImplementedException();
        }

        private GameBoard GetFourPlayerBoard()
        {
            var game = new GameBoard(SetUpFourPlayers());

            return game;
        }

        private List<User> SetUpFourPlayers()
        {
            return new List<User>
            {
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Black, Pass = false},
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Red, Pass = false },
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Blue, Pass = false },
                new User { CurrentOrange = 2, CurrentBrown = 3, CurrentPink = 3, Color = DiscColor.Green, Pass = false },
            };
        }

        //private MapDeck GetTile()

        public void SetGameStatus(string status, int gameId)
        {
            
        }

        public void SetDiscoveryTilesForGame(List<GameDiscovery> tiles)
        {
            Discoveries = tiles;
        }
    }
}
