namespace Dawn.SampleApi.Bootstrap.Tasks
{
    using System;
    using System.Collections.Generic;    
    using System.Web.Http;

    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;

    using Dawn.SampleApi.Controllers;
    using Dawn.WebApi;

    public class ContainerBootstrapTask : IWebApiBootstrapTask
    {
        private readonly IDictionary<Type, Type> typeRegistrations;

        private readonly IDictionary<Type, object> instanceRegistrations;

        public ContainerBootstrapTask(IDictionary<Type, Type> typeRegistrations, IDictionary<Type, object> instanceRegistrations)
        {
            this.typeRegistrations = typeRegistrations ?? new Dictionary<Type, Type>();
            this.instanceRegistrations = instanceRegistrations ?? new Dictionary<Type, object>();
        }

        public void Run(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            builder.RegisterType<DefaultIdentityService>().As<IIdentityService>().SingleInstance();

            this.RegisterAdditional(builder);

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void RegisterAdditional(ContainerBuilder builder)
        {
            foreach (var typeRegistration in this.typeRegistrations)
            {
                builder.RegisterType(typeRegistration.Value).As(typeRegistration.Key);
            }

            foreach (var instanceRegistration in this.instanceRegistrations)
            {
                builder.RegisterInstance(instanceRegistration.Value);
            }
        }
    }
}