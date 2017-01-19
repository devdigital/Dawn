namespace Dawn.SampleApi.IntegrationTests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Dawn.SampleApi.Controllers;
    using Dawn.SampleApi.IntegrationTests.Models;

    using Microsoft.Owin.Testing;

    using Owin;

    using Xunit;

    public class ValuesTests
    {
        [Fact]
        public void ValuesReturns200()
        {
            using (var server = TestServer.Create(this.Boot))
            {
                var response = server.HttpClient.GetAsync("http://localhost:57931/api/values").Result;
                var content = response.Content.ReadAsStringAsync().Result;
                Assert.Equal("\"Test\"", content);
            }
        }

        private void Boot(IAppBuilder app)
        {
            var typeRegistrations = new Dictionary<Type, Type>
            {
                { typeof(IIdentityService), typeof(TestIdentityService) }
            };

            new Bootstrapper(app, typeRegistrations).Run();
        }
    }
}