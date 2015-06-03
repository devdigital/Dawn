namespace Dawn.WebApi
{
    using System.Web.Http;

    public interface IWebApiBootstrapTask
    {
        void Run(HttpConfiguration configuration);
    }
}