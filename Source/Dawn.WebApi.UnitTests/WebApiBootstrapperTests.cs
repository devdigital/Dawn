namespace Dawn.WebApi.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Dawn.WebApi.UnitTests.Attributes;

    using Moq;

    using Xunit;
    using Xunit.Extensions;

    public class WebApiBootstrapperTests
    {
        [Theory]
        [AutoMoqData]
        public void RunWithNoConfigurationThrowsArgumentNullException(
            WebApiBootstrapper webApiBootstrapper,
            List<IWebApiBootstrapTask> tasks)
        {
            Assert.Throws<ArgumentNullException>(() => webApiBootstrapper.Run(null, tasks));
        }

        [Theory]
        [AutoMoqData]
        public void RunWithNoTasksThrowsArgumentNullException(
            WebApiBootstrapper webApiBootstrapper,
            HttpConfiguration configuration)
        {
            Assert.Throws<ArgumentNullException>(() => webApiBootstrapper.Run(configuration, null));
        }

        [Theory]
        [AutoMoqData]
        public void RunWithTasksInvokesAllTasksWithAppBuilder(
            WebApiBootstrapper webApiBootstrapper,
            HttpConfiguration configuration,
            IEnumerable<Mock<IWebApiBootstrapTask>> tasks)
        {
            var enumerable = tasks as Mock<IWebApiBootstrapTask>[] ?? tasks.ToArray();
            webApiBootstrapper.Run(configuration, enumerable.Select(t => t.Object));
            enumerable.ToList().ForEach(t => t.Verify(r => r.Run(It.Is<HttpConfiguration>(c => c == configuration))));
        }
    }
}