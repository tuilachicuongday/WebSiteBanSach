using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanSach.Web.Startup))]
namespace BanSach.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
