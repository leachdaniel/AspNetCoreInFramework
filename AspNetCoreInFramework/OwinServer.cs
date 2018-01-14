using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Owin;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AspNetCoreInFramework
{
    //https://github.com/aspnet/Entropy/blob/900bc664e4d946c79f923f18ca3a9526ec46a09b/samples/Owin.Nowin.HelloWorld/NowinServer.cs
    public class OwinServer : IServer
    {
        public IFeatureCollection Features { get; } = new FeatureCollection();

        public OwinServer()
        {
            Features.Set<IServerAddressesFeature>(new ServerAddressesFeature());
        }

        public Func<IDictionary<string, object>, Task> Invoke; 

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            // Note that this example does not take into account of Nowin's "server.OnSendingHeaders" callback.
            // Ideally we should ensure this method is fired before disposing the context. 
            Invoke = async env =>
            {
                // try without wrapping aspnet trys to add so it doesn't work
                // var features = new OwinFeatureCollection(env);

                // The reason for 2 level of wrapping is because the OwinFeatureCollection isn't mutable
                // so features can't be added
                var owinFeatures = new OwinFeatureCollection(env);

                // enumerating the env owin.ResponseHeaders causes Content-Type to default to text/html which messes up content negotiation
                // set it to empty so AspNetCore can decide the final Content-Type
                owinFeatures.Get<IHttpResponseFeature>().Headers["Content-Type"] = "";

                var features = new FeatureCollection(owinFeatures);
                
                var context = application.CreateContext(features);

                try
                {
                    await application.ProcessRequestAsync(context);
                }
                catch (Exception ex)
                {
                    application.DisposeContext(context, ex);
                    throw;
                }

                application.DisposeContext(context, null);
            };

            // Add the web socket adapter so we can turn OWIN websockets into ASP.NET Core compatible web sockets.
            // The calling pattern is a bit different
            // Invoke = OwinWebSocketAcceptAdapter.AdaptWebSockets(Invoke);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // never gonna stop it
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            StopAsync(CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}