using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeniorProject_ECS.Startup))]
namespace SeniorProject_ECS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
