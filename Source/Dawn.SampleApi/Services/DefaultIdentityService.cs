namespace Dawn.SampleApi.Services
{
    using Dawn.SampleApi.Controllers;

    public class DefaultIdentityService : IIdentityService
    {
        public string UserId => "default";
    }
}