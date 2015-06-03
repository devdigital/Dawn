namespace Dawn.Owin
{
    using System;
    using System.Collections.Generic;

    using global::Owin;

    public class OwinBootstrapper
    {
        public void Run(IAppBuilder app, IEnumerable<IOwinBootstrapTask> tasks)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (tasks == null)
            {
                throw new ArgumentNullException("tasks");
            }
            
            foreach (var task in tasks)
            {
                task.Run(app);
            }            
        }
    }
}