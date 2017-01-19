namespace Dawn.SampleApi.IntegrationTests.Models
{
    using Dawn.SampleApi.Controllers;

    internal class TestIdentityService : IIdentityService
    {
        public string UserId => "test";
    }
}