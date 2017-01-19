namespace Dawn.SampleApi.Extensions
{
    using System.Web.Http.Dependencies;

    internal static class DependencyResolverExtensions
    {
        public static TService GetService<TService>(this IDependencyResolver dependencyResolver) 
            where TService : class
        {
            return dependencyResolver.GetService(typeof(TService)) as TService;
        }        
    }
}