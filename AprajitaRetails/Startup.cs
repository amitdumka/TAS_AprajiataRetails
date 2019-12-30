using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AprajitaRetails.Startup))]
namespace AprajitaRetails
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
