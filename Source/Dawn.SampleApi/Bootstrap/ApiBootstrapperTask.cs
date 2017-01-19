namespace Dawn.SampleApi.Bootstrap
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap.Tasks;
    using Dawn.WebApi;

    using global::Owin;

    public class ApiBootstrapperTask : IOwinBootstrapTask
    {
        public void Run(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            bool includeWebApiLogging = true;
            var tasks = new List<IWebApiBootstrapTask>
            {
                new RoutingWebApiBootstrapTask(),
                new JsonApiBootstrapTask(),
                new LoggingApiBootstrapTask(includeWebApiLogging)
            };

            new WebApiBootstrapper().Run(configuration, tasks);
            app.UseWebApi(configuration);
        }
    }
}