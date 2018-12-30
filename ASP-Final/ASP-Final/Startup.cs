using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_Final.Startup))]
namespace ASP_Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
