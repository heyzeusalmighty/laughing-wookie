using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExploreSpace.Controllers
{
    [Authorize(Roles = "Player, Admin")]
    public class SpaceController : Controller
    {
        // GET: Space
        public ActionResult Index()
        {
            return View();
        }
    }
}