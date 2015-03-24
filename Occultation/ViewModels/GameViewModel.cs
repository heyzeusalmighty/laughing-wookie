using System;
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

        public GameViewModel()
        {
            Repo = new EFGameRepo();
        }
        
        public GameViewModel(IGameRepository repo)
        {
            Repo = repo;
        }

        public List<Game> GetAllGames()
        {
            return Repo.GetAllGames();
        }
    }
}
