using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompressFiles.Startup))]
namespace CompressFiles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
