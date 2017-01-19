namespace Dawn.SampleApi.IntegrationTests.Tests
{
    using System.Net;

    using Microsoft.Owin.Testing;

    using Xunit;

    public class Test
    {
        [Fact]
        public void Foo()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = server.HttpClient.GetAsync("http://localhost:57931/api/values").Result;
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }        
    }
}