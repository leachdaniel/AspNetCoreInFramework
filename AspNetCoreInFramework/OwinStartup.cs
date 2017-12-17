using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Owin;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

// http://aspnet.codeplex.com/SourceControl/latest#Samples/Katana/HelloWorldRawOwin/Startup.cs

[assembly: OwinStartup(typeof(AspNetCoreInFramework.OwinStartup))]
namespace AspNetCoreInFramework
{
    // Note: By default all requests go through this OWIN pipeline. Alternatively you can turn this off by adding an appSetting owin:AutomaticAppStartup with value “false”. 
    // With this turned off you can still have OWIN apps listening on specific routes by adding routes in global.asax file using MapOwinPath or MapOwinRoute extensions on RouteTable.Routes
    
    public class OwinStartup
    {
        // Invoked once at startup to configure your application.
        public void Configuration(IAppBuilder builder)
        {
            aspNetCoreHost = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IServer>(server);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            builder.Use(new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(ignoredNextApp => server.Invoke));

            runTask = aspNetCoreHost.RunAsync();
        }

        private static OwinServer server = new OwinServer();
        private static IWebHost aspNetCoreHost;
        private Task runTask;
    }
}