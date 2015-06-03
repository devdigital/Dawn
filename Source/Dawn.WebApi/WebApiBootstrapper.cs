namespace Dawn.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class WebApiBootstrapper
    {
        public void Run(HttpConfiguration configuration, IEnumerable<IWebApiBootstrapTask> tasks)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (tasks == null)
            {
                throw new ArgumentNullException("tasks");
            }

            foreach (var task in tasks)
            {
                task.Run(configuration);
            }
        }
    }
}