using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hello1.Startup))]
namespace Hello1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
