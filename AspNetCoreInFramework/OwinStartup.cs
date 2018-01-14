using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Owin;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Debug;

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
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        ["Logging"] = @"{
                                ""IncludeScopes"": false,
                                ""LogLevel"": {
                                ""Default"": ""Debug"",
                                ""System"": ""Information"",
                                ""Microsoft"": ""Information""
                        }"
                    });

                    /*config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                            */
                    /*if (env.IsDevelopment())
                    {
                        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }*/

                    config.AddEnvironmentVariables();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     // logging.AddDebug();
                 })
                .UseStartup<AspNetStandard.Startup>()
                .Build();

            builder.Use(new Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>(ignoredNextApp => server.Invoke));

            runTask = aspNetCoreHost.RunAsync();
        }

        private OwinServer server = new OwinServer();
        private IWebHost aspNetCoreHost;
        private Task runTask;
    }
}