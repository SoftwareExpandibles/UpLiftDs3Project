using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Myuken.Startup))]
namespace Myuken
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
