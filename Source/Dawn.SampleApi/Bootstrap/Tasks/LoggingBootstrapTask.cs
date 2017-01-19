namespace Dawn.SampleApi.Bootstrap.Tasks
{
    using System.Web.Http;

    using Dawn.WebApi;

    public class LoggingBootstrapTask : IWebApiBootstrapTask
    {
        private readonly bool includeWebApiLogging;

        public LoggingBootstrapTask(bool includeWebApiLogging)
        {
            this.includeWebApiLogging = includeWebApiLogging;
        }

        public void Run(HttpConfiguration configuration)
        {            
        }
    }
}