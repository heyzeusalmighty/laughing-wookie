using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using Occultation.DAL;
using Occultation.DAL.EF;
using Occultation.DataModels;
using ScienceTrack = Occultation.DataModels.ScienceTrack;

namespace Occultation.ViewModels
{
    public class PlayerViewModel
    {
        private int GameId { get; set; }
        private string GameGuid { get; set; }
        private string UserName { get; set; }
        private int PlayerId { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameBoard CurrentGame { get; set; }
        public IGameRepository Repo { get; set; }
        public ScienceTrack ScienceTrack { get; set; }

        public PlayerViewModel(int gameId, string userName, IGameRepository repo)
        {
            GameId = gameId;
            UserName = userName;
            Repo = repo;
            CurrentGame = Repo.GetGameBoard(GameId);
            CurrentPlayer = Repo.GetCurrentUser(GameId, UserName);
            PlayerId = CurrentPlayer.PlayerId;
            
            ScienceTrack = GetScienceTrack();

        }

        public PlayerViewModel(string gameGuid, string userName, IGameRepository repo)
        {
            GameGuid = gameGuid;
            Repo = repo;
            var currentGame = Repo.GetGame(GameGuid);
            
            GameId = currentGame.GameId;
            UserName = userName;
            
            CurrentGame = Repo.GetGameBoard(GameId);
            CurrentPlayer = Repo.GetCurrentUser(GameId, UserName);
            PlayerId = CurrentPlayer.PlayerId;

            ScienceTrack = GetScienceTrack();

        }

        public ScienceTrack GetScienceTrack()
        {
            var track = new ScienceTrack();
            var tiles = Repo.GetScienceTrack(GameId, PlayerId);

            foreach (var tile in tiles)
            {
                if (tile.Track == Track.Gear)
                {
                    var replace = track.GearTiles.FirstOrDefault(x => x.Position == tile.Position);
                    if (replace != null)
                    {
                        replace.Name = tile.Name;
                        replace.Image = tile.Image;
                    }
                    
                }
                else if (tile.Track == Track.Grid)
                {
                    var replace = track.GridTiles.FirstOrDefault(x => x.Position == tile.Position);
                    if (replace != null)
                    {
                        replace.Name = tile.Name;
                        replace.Image = tile.Image;
                    }
                }
                else
                {
                    //its a star!!!
                    var replace = track.StarTiles.FirstOrDefault(x => x.Position == tile.Position);
                    if (replace != null)
                    {
                        replace.Name = tile.Name;
                        replace.Image = tile.Image;
                    }
                }
            }
            return track;
        }
       

    }
}
