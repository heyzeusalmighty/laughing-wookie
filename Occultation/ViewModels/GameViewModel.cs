﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Occultation.DAL;
using Occultation.DAL.EF;

namespace Occultation.ViewModels
{
    public class GameViewModel
    {
        private IGameRepository Repo { get; set; }
        private List<GameUser> Users; 

        public GameViewModel()
        {
            Repo = new EFGameRepo();
            Users = Repo.GetAllGameUsers();
        }
        
        public GameViewModel(IGameRepository repo)
        {
            Repo = repo;
          
        }

        public List<Game> GetAllGames()
        {
            return Repo.GetAllGames();
        }

        public string ResetGames()
        {
            CleanUpDatabase();
            
            //BuildGame(2);
            //BuildGame(3);
            //BuildGame(4);
            //BuildGame(5);
            //BuildGame(6);
            var repo = new EFGameRepo();
            var gameCreator = repo.CreateGame("For Testing");
            var game = repo.GetGame(gameCreator);

            var creatorModel = new GameCreator(repo);
            creatorModel.AddPlayerToGame(1, game.GameIdentifier);
            creatorModel.AddPlayerToGame(2, game.GameIdentifier);
            creatorModel.AddPlayerToGame(3, game.GameIdentifier);
            creatorModel.AddPlayerToGame(4, game.GameIdentifier);
            creatorModel.AddPlayerToGame(5, game.GameIdentifier);
            creatorModel.AddPlayerToGame(6, game.GameIdentifier);

            return "yay";
        }


        private void BuildGame(int playerCount)
        {
            string title = "Dummy Name";
            switch (playerCount)
            {
                case 2:
                    title = "Two Players";
                    break;
                case 3:
                    title = "Three Players";
                    break;
                case 4:
                    title = "Four Players";
                    break;
                case 5:
                    title = "Five Players";
                    break;
                case 6:
                    title = "Six Players";
                    break;
                default:
                    title = "Dummy Name";
                    break;
            }
            
            var gameId = Repo.CreateGame(title);
            var game = Repo.GetGame(gameId);
            var creator = new GameCreator(Repo);

            foreach (var user in Users.Take(playerCount))
            {
                creator.AddPlayerToGame(user.UserId, game.GameIdentifier);
            }

            var start = new StartGameJob(Repo);
            start.StartGame(game.GameIdentifier);
        }


        private void CleanUpDatabase()
        {

            using (var context = new GameModel())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Game]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [MapDeck]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Player]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerShipModel]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerTrack]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ScienceTrack]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ShipModelComponents]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Tiles]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PlayerShips]");
            }


        }

    }
}
