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

        public PlayerViewModel GetPlayer()
        {
            var repo = new FakeGameRepo();
            var model = new PlayerViewModel(0, User.Identity.Name, repo);
            return model;
        }
    }
}
