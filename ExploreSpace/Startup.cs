using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExploreSpace.Startup))]
namespace ExploreSpace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}