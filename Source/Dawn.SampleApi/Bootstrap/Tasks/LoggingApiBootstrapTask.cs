namespace Dawn.SampleApi.Bootstrap.Tasks
{
    using System.Web.Http;

    using Dawn.WebApi;

    public class LoggingApiBootstrapTask : IWebApiBootstrapTask
    {
        private readonly bool includeWebApiLogging;

        public LoggingApiBootstrapTask(bool includeWebApiLogging)
        {
            this.includeWebApiLogging = includeWebApiLogging;
        }

        public void Run(HttpConfiguration configuration)
        {            
        }
    }
}