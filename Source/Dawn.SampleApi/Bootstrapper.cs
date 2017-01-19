namespace Dawn.SampleApi
{
    using System;
    using System.Collections.Generic;

    using Dawn.Owin;
    using Dawn.SampleApi.Bootstrap;

    using global::Owin;    

    public class Bootstrapper
    {
        private IAppBuilder app;

        private readonly IDictionary<Type, Type> typeRegistrations;

        private readonly IDictionary<Type, object> instanceRegistrations;        

        public Bootstrapper(IAppBuilder app, IDictionary<Type, Type> typeRegistrations = null, IDictionary<Type, object> instanceRegistrations = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException();
            }

            this.app = app;
            this.typeRegistrations = typeRegistrations;
            this.instanceRegistrations = instanceRegistrations;
        }

        public void Run()
        {
            var tasks = new[]
            {
                new ApiBootstrapperTask(this.typeRegistrations, this.instanceRegistrations)
            };

            new OwinBootstrapper().Run(this.app, tasks);
        }
    }
}