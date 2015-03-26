using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Occultation.ViewModels;

namespace ExploreSpace.Controllers
{
    public class GameController : Controller
    {


        public JsonResult GetAllGames()
        {
            var model = new GameViewModel();
            return Json(model.GetAllGames(), JsonRequestBehavior.AllowGet);
        }

        public string ResetGames()
        {
            var model = new GameViewModel();
            return model.ResetGames();
        }

    }
}