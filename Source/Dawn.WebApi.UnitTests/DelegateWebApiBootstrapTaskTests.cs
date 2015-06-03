namespace Dawn.WebApi.UnitTests
{
    using System;
    using System.Web.Http;

    using Dawn.WebApi.UnitTests.Attributes;

    using Xunit;
    using Xunit.Extensions;

    public class DelegateWebApiBootstrapTaskTests
    {
        [Fact]
        public void ImplementsIWebApiBootstrapTask()
        {
            Assert.True(typeof(IWebApiBootstrapTask).IsAssignableFrom(typeof(DelegateWebApiBootstrapTask)));
        }

        [Theory]
        [AutoMoqData]
        public void InstantiateWithoutTaskThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DelegateWebApiBootstrapTask(null));
        }

        [Theory]
        [AutoMoqData]
        public void RunInvokesDelegateWithHttpConfiguration(HttpConfiguration configuration)
        {
            var x = false;
            var delegateTask = new DelegateWebApiBootstrapTask(c => x = c == configuration);
            delegateTask.Run(configuration);
            Assert.True(x);
        }
    }
}