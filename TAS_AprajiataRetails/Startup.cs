using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TAS_AprajiataRetails.Startup))]
namespace TAS_AprajiataRetails
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
