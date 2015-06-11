# Dawn
Dawn is a set of simple bootstrapping helpers for .NET applications.

# Quickstart

## Dawn.Owin

1. Install the [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/) NuGet package.
2. Create *IOwinBootstrapTask* tasks for each section of your bootstrapping process.
3. Bootstrap from your OWIN startup class with the *OwinBootstrapper*, specifying the order of the bootstrapping tasks.

**Example OWIN Startup Class**
```csharp
public class Startup
{
  public void Configuration(IAppBuilder app)
  {
    var tasks = new List<IOwinBootstrapTask> { new ApiBootstrapperTask(), new WebBootstrapperTask() };
    new OwinBootstrapper().Run(app, tasks);
  }
}
```

**Example Bootstrapper Tasks**



# Downloads
Dawn is available via NuGet:

* [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/)
* [Dawn.WebApi](https://www.nuget.org/packages/Dawn.WebApi/)
