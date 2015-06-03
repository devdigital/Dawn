namespace Dawn.Owin
{
    using global::Owin;

    public interface IOwinBootstrapTask
    {
        void Run(IAppBuilder app);
    }
}