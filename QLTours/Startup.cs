using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLTours.Startup))]
namespace QLTours
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
