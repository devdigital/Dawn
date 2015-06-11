# Dawn
Dawn is a set of simple bootstrapping helpers for .NET applications.

# Quickstart

## Dawn.Owin

1. Install the [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/) NuGet package.
2. Create ordrered *IOwinBootstrapTask* tasks for each step of your bootstrapping process.
3. Bootstrap from your OWIN startup class with the *OwinBootstrapper*, providing the *IAppBuilder* instance and task collection.

**Example OWIN Startup Class**

Here we're bootstrapping an API (ASP.NET Web API) and a web project, in the order specified. We first define the tasks in the appropriate order as instances of *IOwinBootstrapTask*. Then we invoke the *Run* method of the *OwinBootstrapper* to execute the tasks.

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

Our bootstrapper tasks are instances of *IOwinBootstrapperTask*. We must implement one *Run* method which shares the same instance of the OWIN *IAppBuilder* across all tasks.

In this example our API bootstrapper configures an API using the ASP.NET Web API extension method *UseWebApi*. The web boostrapper task is using [NancyFx](http://nancyfx.org/) to host static web pages, using the *UseNancy* extension method.

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

## Dawn.WebApi

1. Install the [Dawn.WebApi](https://www.nuget.org/packages/Dawn.WebApi/) NuGet package.
2. Create ordered *IWebApiBootstrapTask* tasks for each step of your API bootstrapping process.
3. Bootstrap from your API OWIN bootstrapping task with the *WebApiBootstrapper*, providing an instance of an *HttpConfiguration* and task collection.

**Example API Bootstrapper**

Here we extend the example API bootstrapper task above to help bootstrap our ASP.NET Web API. This allows us to have separate *IWebApiBootstrapTask* tasks for each area of API configuration.

We specify the order in which we want the tasks bootstrapped, and then invoke the *Run* method of the *WebApiBootstrapper* to execute the tasks.

```csharp
public class ApiBootstrapperTask : IOwinBootstrapTask
{        
    public void Run(IAppBuilder app)
    {            
        var httpConfiguration = new HttpConfiguration();

        var tasks = new List<IWebApiBootstrapTask>
            {
                new RoutingApiBootstrapTask(),
                new JsonApiBootstrapTask()
            };

        new WebApiBootstrapper().Run(httpConfiguration, tasks);
        app.UseWebApi(httpConfiguration);
    }
}
```

**Example API Bootstrapper Tasks**

Our API bootstrapper tasks are instances of *IWebApiBootstrapTask*. We must implement one *Run* method which shares the same instance of the *HTTPConfiguration* across all tasks.

In this example our API bootstrapper tasks include a routing task to configure attribute routing, and a JSON configuration task which enables camel casing JSON serialization.

```csharp
public class RoutingApiBootstrapTask : IWebApiBootstrapTask
{
    public void Run(HttpConfiguration configuration)
    {
        configuration.MapHttpAttributeRoutes();
    }
}

public class JsonApiBootstrapTask : IWebApiBootstrapTask
{
    public void Run(HttpConfiguration configuration)
    {
        var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}
```

# Downloads
Dawn is available via NuGet:

* [Dawn.Owin](https://www.nuget.org/packages/Dawn.Owin/)
* [Dawn.WebApi](https://www.nuget.org/packages/Dawn.WebApi/)
