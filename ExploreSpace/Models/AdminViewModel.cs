using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExploreSpace.Models
{
    public class AdminViewModel
    {

        public AdminViewModel()
        {
            
        }


        public string GetUserRoles(string userId)
        {
            using (var db = new ApplicationDbContext())
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
        }


        public List<AdminPlayer> GetAllTheUsers()
        {
            using (var db = new ApplicationDbContext())
            {
                var toBuild = new List<AdminPlayer>();
                var other = db.Users.Include("PlayerInfo").ToList();
                foreach (var dummy in other)
                {
                    var playerInfo = db.PlayerInformations.FirstOrDefault(x => x.UserIdentity == dummy.Id);

                    if (playerInfo != null)
                    {
                        var userName = dummy.UserName;



                        var builder = new AdminPlayer
                        {
                            UserName = userName,
                            PlayerId = playerInfo.Id,
                            ContactEmail = playerInfo.ContactEmailAddress,
                            IsActive = playerInfo.IsActive,
                            Roles = GetUserRoles(dummy.Id)
                        };
                        toBuild.Add(builder);
                    }
                }

                return toBuild;

            }
        }

    }
}