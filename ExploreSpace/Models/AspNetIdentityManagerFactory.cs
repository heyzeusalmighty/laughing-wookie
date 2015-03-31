using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Thinktecture.IdentityManager;

namespace ExploreSpace.Models
{
    public class AspNetIdentityManagerFactory
    {
        string connString;

        public AspNetIdentityManagerFactory(string connString)
        {
            this.connString = connString;
        }

        public IIdentityManagerService Create()
        {
            var db = new IdentityDbContext<IdentityUser>(connString);
            var userStore = new UserStore<IdentityUser>(db);
            var userMgr = new Microsoft.AspNet.Identity.UserManager<IdentityUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleMgr = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(roleStore);

            var service =
                new IdentityManager.AspNetIdentity.AspNetIdentityManagerService
                    <IdentityUser, string, IdentityRole, string>(userMgr, roleMgr);

            return new DisposableIdentityManagerService(service, db);


        }
    }


    public class AspNetIdentityIdentityManagerFactory
    {
        string connString;

        public AspNetIdentityIdentityManagerFactory(string connString)
        {
            this.connString = connString;
        }

        public IIdentityManagerService Create()
        {
            var db = new IdentityDbContext<IdentityUser>(connString);
            var userStore = new UserStore<IdentityUser>(db);
            var userMgr = new Microsoft.AspNet.Identity.UserManager<IdentityUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleMgr = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(roleStore);

            var svc = new Thinktecture.IdentityManager.AspNetIdentity.AspNetIdentityManagerService<IdentityUser, string, IdentityRole, string>(userMgr, roleMgr);


            return new DisposableIdentityManagerService(svc, db);
        }
    }
}