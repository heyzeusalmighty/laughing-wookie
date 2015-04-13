using System;
using System.Collections.Generic;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace Occultation.DAL
{
    public interface IGameRepository : IDisposable
    {
        void Save();
        GameBoard GetGameBoard(int gameId);
        Player GetCurrentUser(int gameId, string userName);
        List<ScienceTile> GetScienceTrack(int gameId, int playerId);
        
        int CreateGame(string title);
        string AddPlayerToGame(int userId, int gameId);
        List<Player> GetPlayersForGame(int gameId);
        Game GetGame(string gameGuid);
        Game GetGame(int gameId);
        Game GetLastGame();
        void AddScienceTileToTrack(PlayerTrack track);
        PlayerShipModel AddNewShipModel(PlayerShipModel model, List<ShipComponent> components);
        void AddTilesToNewGame(List<MapDeck> tiles);
        List<MapDeck> GetRevealedTiles(int gameId);
        MapDeck GetNextTile(int gameId, int div, int x, int y);
        void SetPlayerColor(int playerId, string color);
        List<GameUser> GetAllGameUsers();
        List<Game> GetAllGames();
        void SaveNewShip(PlayerShip ship);

    }
}
