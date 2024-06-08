using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProjectn01681774.Startup))]
namespace PassionProjectn01681774
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
