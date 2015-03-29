using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExploreSpace.Models
{
    public class AdminViewModel
    {

        public List<string> Rolls { get; set; }
        public List<SimpleUser> Users { get; set; }

        private ApplicationDbContext db;
        public AdminViewModel()
        {
            db = new ApplicationDbContext();
            GetAllRolesAndUsers();
        }


        public void GetAllRolesAndUsers()
        {
            Rolls = db.Roles.Select(x => x.Name).ToList();
            Users = db.Users.Select(x => new SimpleUser{ Email = x.Email, UserName = x.UserName}).ToList();
        }

        public List<IdentityRole> GetAllRoles()
        {
            return db.Roles.ToList();
        }


        public string GetUserRoles(string userId)
        {
           
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var allThoseRoles = um.GetRoles(userId);

            var rollList = "";

            foreach (var roll in allThoseRoles)
            {
                rollList += roll + ", ";
            }

            var catconcat = rollList.Length < 2 ? rollList : rollList.Substring(0, rollList.Length - 2);

            return catconcat;

            
        }


        public string AddUserToRole(string userName, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            try
            {
                var user = userManager.FindByName(userName);
                userManager.AddToRole(user.Id, roleName);
                db.SaveChanges();
                return "Success";
            }
            catch
            {
                throw;
                return "Ruh roh";
            }
        }

    }
}