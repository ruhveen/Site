using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesterMatching.Startup))]
namespace TesterMatching
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
