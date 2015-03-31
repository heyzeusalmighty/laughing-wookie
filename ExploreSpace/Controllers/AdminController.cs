using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExploreSpace.Models;

namespace ExploreSpace.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsersAndRolls()
        {
            var model = new AdminViewModel();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public string AddUserToRole(string userName, string roleName)
        {
            var vm = new AdminViewModel();
            return vm.AddUserToRole(userName, roleName);
        }

        public string RemoveUserFromRole(string userName, string roleName)
        {
            var vm = new AdminViewModel();
            return vm.RemoveUserFromRole(userName, roleName);
        }
    }
}