using System;
using System.Collections.Generic;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.DAL
{
    public interface IGameRepository : IDisposable
    {
        GameBoard GetGameBoard(int gameId);
        Player GetCurrentUser(int gameId, string userName);
        List<ScienceTile> GetScienceTrack(int gameId, int playerId);
        
        int CreateGame();
        string AddPlayerToGame(int userId, int gameId, string color);
        List<Player> GetPlayersForGame(int gameId);
        Game GetGame(string gameGuid);
        void AddScienceTileToTrack(PlayerTrack track);
        void AddNewShipModel(PlayerShipModel model, List<ShipComponent> components);
        void AddTilesToNewGame(List<MapDeck> tiles);
        List<MapDeck> GetRevealedTiles(int gameId);
    }
}
