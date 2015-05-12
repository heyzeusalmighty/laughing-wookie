using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ExploreSpace.Controllers
{
    public class TurnController : Controller
    {

        public JsonResult Explore(int division, int gameId)
        {
            var player = User.Identity.Name;

            return Json(player);
        }

    }
}