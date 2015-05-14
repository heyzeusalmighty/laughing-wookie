using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExploreSpace.Models;
using Newtonsoft.Json;
using NLog;
using Occultation.DataModels;

namespace ExploreSpace.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            _logger.Info("hello from Admin");
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

        public string AddNewRole(string roleName)
        {
            var vm = new AdminViewModel();
            return vm.AddNewRole(roleName);
        }

        public JsonResult GetEmailSettings()
        {
            var vm = new AdminViewModel();
            return Json(vm.GetEmailSettings(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateEmailSettings(string settings)
        {
            var set = JsonConvert.DeserializeObject<EmailSettings>(settings);
            var vm = new AdminViewModel();
            vm.UpdateEmailSettings(set);
        }
    }
}