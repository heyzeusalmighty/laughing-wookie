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

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Users = db.Users.Select(x => new SimpleUser{ Email = x.Email, UserName = x.UserName}).ToList();
            Users = new List<SimpleUser>();
            foreach (var user in db.Users)
            {
                //user.IsAdmin = userManager.IsInRole(user., "Admin");
                //var isAdmin = userManager.IsInRole(user.Id, "Admin");
                Users.Add(new SimpleUser
                {
                    Email = user.Email, 
                    UserName = user.UserName, 
                    IsAdmin = userManager.IsInRole(user.Id, "Admin"),
                    IsPlayer = userManager.IsInRole(user.Id, "Player")
                });
            }
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
                return "Added";
            }
            catch
            {
                throw;
                return "Ruh roh";
            }
        }

        public string RemoveUserFromRole(string userName, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            try
            {
                var user = userManager.FindByName(userName);
                userManager.RemoveFromRole(user.Id, roleName);
                db.SaveChanges();
                return "Removed";
            }
            catch (Exception)
            {
                throw;
                return "Ruh roh";
            }
        }

        public string AddNewRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityResult roleResult;
            if (!roleManager.RoleExists(roleName))
            {
                roleResult = roleManager.Create(new IdentityRole(roleName));
                return "Success";
            }
            return "Already Exists";
        }

    }
}