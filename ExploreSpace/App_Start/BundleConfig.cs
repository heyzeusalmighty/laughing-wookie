using System.Web;
using System.Web.Optimization;


namespace ExploreSpace
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/toaster.min.css",
                "~/Content/spaceStyles.css"
            ));


            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular-1.3.11.min.js",
                "~/Scripts/ui-router-0.2.13.min.js",
                "~/Scripts/angular-animate-1.3.11.min.js",
                "~/Scripts/angToaster.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/spaceApp").Include(
               "~/Scripts/Space/hexMap.js",
                "~/Scripts/Space/spaceRoutes.js",
                "~/Scripts/Space/spaceFactory.js",
                "~/Scripts/Space/Controllers/homeController.js",
                "~/Scripts/Space/Controllers/messagesController.js",
                "~/Scripts/Space/Controllers/profileController.js",
                "~/Scripts/Space/Controllers/shipsController.js",
                "~/Scripts/Space/Controllers/mapController.js",
                "~/Scripts/Space/Controllers/menuController.js",
                "~/Scripts/Space/Controllers/gameController.js",
                "~/Scripts/Space/Controllers/exploreController.js"
               ));

            BundleTable.EnableOptimizations = true;
        }

    }
}