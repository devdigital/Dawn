namespace Dawn.SampleApi.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap.Tasks;
    using Dawn.SampleApi.Services;
    using Dawn.WebApi;

    using global::Owin;

    public class ApiBootstrapperTask : IOwinBootstrapTask
    {
        private readonly IDictionary<Type, object> instanceRegistrations;

        private readonly IDictionary<Type, Type> typeRegistrations;

        public ApiBootstrapperTask(IDictionary<Type, Type> typeRegistrations, IDictionary<Type, object> instanceRegistrations)
        {
            this.typeRegistrations = typeRegistrations;
            this.instanceRegistrations = instanceRegistrations;
        }

        public void Run(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            
            new ContainerBootstrapTask(this.typeRegistrations, this.instanceRegistrations).Run(configuration);

            var configurationService = configuration.DependencyResolver.GetService<IConfigurationService>();
            if (configurationService == null)
            {
                throw new ArgumentNullException($"No {typeof(IConfigurationService).Name} registered in the container");
            }

            var tasks = new List<IWebApiBootstrapTask>
            {
                new RoutingWebApiBootstrapTask(),
                new JsonBootstrapTask(),
                new LoggingBootstrapTask(configurationService.GetSetting("dawn:WebApiLogging", false))
            };
            
            new WebApiBootstrapper().Run(configuration, tasks);
            app.UseWebApi(configuration);
        }
    }

    internal static class DependencyResolverExtensions
    {
        public static TService GetService<TService>(this IDependencyResolver dependencyResolver) 
            where TService : class
        {
            return dependencyResolver.GetService(typeof(TService)) as TService;
        }        
    }
}