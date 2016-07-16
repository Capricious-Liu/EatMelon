using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EatMelon.Startup))]
namespace EatMelon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
