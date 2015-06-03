namespace Dawn.WebApi
{
    using System;

    using System.Web.Http;

    public class DelegateWebApiBootstrapTask : IWebApiBootstrapTask
    {
        private Action<HttpConfiguration> task;

        public DelegateWebApiBootstrapTask(Action<HttpConfiguration> task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }

            this.task = task;
        }

        public void Run(HttpConfiguration configuration)
        {
            this.task(configuration);
        }
    }
}