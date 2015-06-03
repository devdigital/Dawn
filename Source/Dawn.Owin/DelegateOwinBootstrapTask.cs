namespace Dawn.Owin
{
    using System;

    using global::Owin;

    public class DelegateOwinBootstrapTask : IOwinBootstrapTask
    {
        private Action<IAppBuilder> task;

        public DelegateOwinBootstrapTask(Action<IAppBuilder> task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }

            this.task = task;
        }

        public void Run(IAppBuilder app)
        {
            this.task(app);
        }
    }
}