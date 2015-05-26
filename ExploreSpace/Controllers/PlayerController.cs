using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Occultation.DAL;
using Occultation.ViewModels;

namespace ExploreSpace.Controllers
{
    public class PlayerController : ApiController
    {

        public PlayerViewModel GetPlayer(string name, string gameId)
        {
            var repo = new EFGameRepo();
            var model = new PlayerViewModel(gameId, name, repo);
            return model;
        }
    }
}
