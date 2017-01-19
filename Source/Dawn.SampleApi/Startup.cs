using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Dawn.SampleApi.Startup))]

namespace Dawn.SampleApi
{
    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new Bootstrapper(app).Run();
        }
    }
}
