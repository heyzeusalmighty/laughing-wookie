using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Occultation.DAL.EF;
using Occultation.DataModels;

namespace ExploreSpace.Models
{
    public class AdminViewModel
    {

        public List<string> Rolls { get; set; }
        public List<SimpleUser> Users { get; set; }

        public AdminViewModel()
        {
            
            GetAllRolesAndUsers();
        }


        public void GetAllRolesAndUsers()
        {
            using (var db = new ApplicationDbContext())
            {
                Rolls = db.Roles.Select(x => x.Name).ToList();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                Users = new List<SimpleUser>();
                var allUsers = db.Users.ToList();
                foreach (var user in allUsers)
                {

                    Users.Add(new SimpleUser
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        IsAdmin = userManager.IsInRole(user.Id, "Admin"),
                        IsPlayer = userManager.IsInRole(user.Id, "Player")
                    });
                }
            }
            
        }

        public List<IdentityRole> GetAllRoles()
        {
            using (var db = new ApplicationDbContext())
            {

                return db.Roles.ToList();   
            }
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


        public string AddUserToRole(string userName, string roleName)
        {
            using (var db = new ApplicationDbContext())
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
        }

        public string RemoveUserFromRole(string userName, string roleName)
        {
            using (var db = new ApplicationDbContext())
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
        }

        public string AddNewRole(string roleName)
        {
            using (var db = new ApplicationDbContext())
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

        public EmailSettings GetEmailSettings()
        {
            using (var model = new GameModel())
            {
                return model.EmailSettings.First();
            }
        }


        public void UpdateEmailSettings(EmailSettings settings)
        {
            using (var model = new GameModel())
            {
                var oldSettings = model.EmailSettings.First();
                if (oldSettings != null)
                {
                    oldSettings.Address = settings.Address;
                    oldSettings.Password = settings.Password;
                    oldSettings.Sender = settings.Sender;
                    oldSettings.UserName = settings.UserName;
                    oldSettings.AdminEmail = settings.AdminEmail;
                    model.SaveChanges();
                }
                else
                {
                    model.EmailSettings.Add(settings);
                    model.SaveChanges();
                }
            }
        }

        public void SendNewUserEmail(string username)
        {
            using (var db = new GameModel())
            {
                var settings = db.EmailSettings.First();

                if (settings != null)
                {
                    // Configure the client:
                    System.Net.Mail.SmtpClient client =
                        new System.Net.Mail.SmtpClient(settings.Address);


                    client.Port = 587;
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;

                    // Create the credentials:
                    System.Net.NetworkCredential credentials =
                        new System.Net.NetworkCredential(settings.UserName, settings.Password);

                    //client.EnableSsl = true;
                    client.Credentials = credentials;

                    // Create the message:
                    var mail =
                        new System.Net.Mail.MailMessage(settings.Sender, settings.AdminEmail);

                    mail.Subject = "New User Has Signed Up";
                    mail.Body = username + " has signed up";

                    // Send first email:
                    client.Send(mail);

                    
                }

                

            }
        }
    }
}