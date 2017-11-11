using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullStack.Startup))]
namespace FullStack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
