namespace Dawn.Owin.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Dawn.Owin;
    using Dawn.Owin.UnitTests.Attributes;

    using global::Owin;

    using Moq;

    using Xunit;
    using Xunit.Extensions;

    public class OwinBootstrapperTests
    {
        [Theory]
        [AutoMoqData]
        public void RunWithNoAppBuilderThrowsArgumentNullException(
            OwinBootstrapper owinBootstrapper, 
            List<IOwinBootstrapTask> tasks)
        {
            Assert.Throws<ArgumentNullException>(() => owinBootstrapper.Run(null, tasks));
        }

        [Theory]
        [AutoMoqData]
        public void RunWithNoTasksThrowsArgumentNullException(
            OwinBootstrapper owinBootstrapper, 
            IAppBuilder app)
        {
            Assert.Throws<ArgumentNullException>(() => owinBootstrapper.Run(app, null));
        }

        [Theory]
        [AutoMoqData]
        public void RunWithTasksInvokesAllTasksWithAppBuilder(
            OwinBootstrapper owinBootstrapper, 
            IAppBuilder app,
            IEnumerable<Mock<IOwinBootstrapTask>> tasks)
        {
            var enumerable = tasks as Mock<IOwinBootstrapTask>[] ?? tasks.ToArray();
            owinBootstrapper.Run(app, enumerable.Select(t => t.Object));
            enumerable.ToList().ForEach(t => t.Verify(r => r.Run(It.Is<IAppBuilder>(a => a == app))));
        }
    }
}