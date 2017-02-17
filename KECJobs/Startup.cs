using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KECJobs.Startup))]
namespace KECJobs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
