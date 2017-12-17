using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Web;
using Microsoft.AspNetCore.Http.Features;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace AspNetCoreInFramework
{
    public class IISHandlerServer : IServer
    {
        public IFeatureCollection Features => new FeatureCollection();

        public IISHandlerServer()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            Features.Set<IHttpRequestFeature>(new IISHttpRequestFeature());
            Features.Set<IHttpResponseFeature>(new IISHttpResponseFeature());

            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
