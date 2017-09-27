using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeniorProjectECS.Startup))]
namespace SeniorProjectECS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
