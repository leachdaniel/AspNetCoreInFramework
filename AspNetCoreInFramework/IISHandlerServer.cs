using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Web;
using Microsoft.AspNetCore.Http.Features;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Owin;

namespace AspNetCoreInFramework
{
    public class IISHandlerServer : IServer
    {
        public IFeatureCollection Features => new FeatureCollection();

        public IISHandlerServer()
        {
        }

        public Action<HttpContext> ProcessRequest;


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            ProcessRequest = async httpContext =>
            {
                //var builder = new HttpContextBuilder(_application);
                //builder.Configure(context =>
                //{
                //    var request = context.Request;
                //    request.Scheme = BaseAddress.Scheme;
                //    request.Host = HostString.FromUriComponent(BaseAddress);
                //    if (BaseAddress.IsDefaultPort)
                //    {
                //        request.Host = new HostString(request.Host.Host);
                //    }
                //    var pathBase = PathString.FromUriComponent(BaseAddress);
                //    if (pathBase.HasValue && pathBase.Value.EndsWith("/"))
                //    {
                //        pathBase = new PathString(pathBase.Value.Substring(0, pathBase.Value.Length - 1));
                //    }
                //    request.PathBase = pathBase;
                //});

                // The reason for 2 level of wrapping is because the OwinFeatureCollection isn't mutable
                // so features can't be added
                //var features = new FeatureCollection(new OwinFeatureCollection(env));

                //var context = application.CreateContext(features);

                
                //try
                //{
                //    await application.ProcessRequestAsync(context);
                //}
                //catch (Exception ex)
                //{
                //    application.DisposeContext(context, ex);
                //    throw;
                //}

                //application.DisposeContext(context, null);
            };

            // Add the web socket adapter so we can turn OWIN websockets into ASP.NET Core compatible web sockets.
            // The calling pattern is a bit different
            // Invoke = OwinWebSocketAcceptAdapter.AdaptWebSockets(Invoke);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
