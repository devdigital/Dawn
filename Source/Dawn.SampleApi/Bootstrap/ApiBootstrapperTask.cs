namespace Dawn.SampleApi.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap.Tasks;
    using Dawn.WebApi;

    using global::Owin;

    public class ApiBootstrapperTask : IOwinBootstrapTask
    {
        private IDictionary<Type, object> instanceRegistrations;

        private IDictionary<Type, Type> typeRegistrations;

        public ApiBootstrapperTask(IDictionary<Type, Type> typeRegistrations, IDictionary<Type, object> instanceRegistrations)
        {
            this.typeRegistrations = typeRegistrations;
            this.instanceRegistrations = instanceRegistrations;
        }

        public void Run(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            bool includeWebApiLogging = true;
            var tasks = new List<IWebApiBootstrapTask>
            {
                new ContainerBootstrapTask(this.typeRegistrations, this.instanceRegistrations),
                new RoutingWebApiBootstrapTask(),
                new JsonBootstrapTask(),
                new LoggingBootstrapTask(includeWebApiLogging)
            };

            new WebApiBootstrapper().Run(configuration, tasks);
            app.UseWebApi(configuration);
        }
    }
}