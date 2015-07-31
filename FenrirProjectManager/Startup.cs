using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FenrirProjectManager.Startup))]
namespace FenrirProjectManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
