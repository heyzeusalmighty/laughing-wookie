using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Occultation.DataModels;

namespace ExploreSpace.Controllers
{
    public class TurnController : Controller
    {

        public JsonResult Explore(int x, int y, string gameId)
        {
            var player = User.Identity.Name;

            return Json(player);
        }

        public JsonResult GetExploreNeighbors(string gameId)
        {
            var player = User.Identity.Name;

            player = "TeddyRoooo";

            var turns = new TakingTurns();
            return Json(turns.GetExploreOptions(gameId, player), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExploreNeighborsByPlayer(string gameId, string player)
        {
            var turns = new TakingTurns();
            return Json(turns.GetExploreOptions(gameId, player), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExploreByPlayer(int x, int y, string gameId, string player)
        {
            var turns = new TakingTurns();
            var explore = turns.ExploreTile(player, gameId, x, y);
            return Json(explore);
        }

        [HttpPost]
        public string SetWormholeIndex(int map, int index, string gameId, string name)
        {
            var turns = new TakingTurns();
            return turns.SetWormholeIndex(map, index, gameId, name);
        }

        [HttpPost]
        public JsonResult InfluenceTileFromExplore(int map, string gameId, string name, bool answer)
        {
            var turns = new TakingTurns();
            var influencing = turns.InfluenceTileFromExplore(map, gameId, name, answer);
            return Json(influencing);
        }
    }
}