namespace Dawn.Owin.UnitTests
{
    using System;

    using Dawn.Owin.UnitTests.Attributes;

    using global::Owin;

    using Xunit;
    using Xunit.Extensions;

    public class DelegateOwinBootstrapTaskTests
    {
        [Fact]        
        public void ImplementsIOwinBootstrapTask()
        {
            Assert.True(typeof(IOwinBootstrapTask).IsAssignableFrom(typeof(DelegateOwinBootstrapTask)));
        }

        [Theory]
        [AutoMoqData]
        public void InstantiateWithoutTaskThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DelegateOwinBootstrapTask(null));
        }

        [Theory]
        [AutoMoqData]
        public void RunInvokesDelegateWithAppBuilder(IAppBuilder app)
        {
            var x = false;
            var delegateTask = new DelegateOwinBootstrapTask(a => x = a == app);
            delegateTask.Run(app);
            Assert.True(x);
        }
    }
}