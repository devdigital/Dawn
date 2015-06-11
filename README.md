# Dawn
Dawn is a set of simple bootstrapping helpers for .NET applications.

# Quickstart

## Dawn.Owin

1. Install the [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/) NuGet package.
2. Create *IOwinBootstrapTask* tasks for each section of your bootstrapping process.
3. Bootstrap from your OWIN startup class with the *OwinBootstrapper*, specifying the order of the bootstrapping tasks.

**Example OWIN Startup Class**

Here we're bootstrapping an API (ASP.NET Web API) and a web project, in the order specified.

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

Our bootstrapper tasks are instances of *IOwinBootstrapperTask*. We must implement one *Run* method that takes the same instance of the OWIN *IAppBuilder*.

In this example our API bootstrapper configures an API using the ASP.NET Web API extension method *UseWebApi*. The web boostrapper task is using [NancyFx](http://nancyfx.org/) in this case to host static web pages, using the *UseNancy* extension method.

```csharp
public class ApiBootstrapperTask : IOwinBootstrapTask
{        
    public void Run(IAppBuilder app)
    {            
        var httpConfiguration = new HttpConfiguration();
        ...
        app.UseWebApi(httpConfiguration);
    }
}

public class WebBootstrapperTask : IOwinBootstrapTask
{
    public void Run(IAppBuilder app)
    {
        app.UseNancy();
        app.UseStageMarker(PipelineStage.MapHandler);
    }
}
```

# Downloads
Dawn is available via NuGet:

* [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/)
* [Dawn.WebApi](https://www.nuget.org/packages/Dawn.WebApi/)
