using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AspNetCoreInFramework
{
    // todo implement IHttpAsyncHandler instead
    // examples
    // https://github.com/ServiceStack/ServiceStack/blob/1d50fa80ebcc8012f89e71c5a7f129558b741079/src/ServiceStack/Host/Handlers/RequestInfoHandler.cs
    // https://github.com/ServiceStack/ServiceStack/blob/191641dd87aff6d967f4dc7e2b60db4adca88a91/src/ServiceStack/Host/Handlers/HttpAsyncTaskHandler.cs
    // has some net core code as well as IHttpAsyncHandler:
    // https://github.com/ServiceStack/ServiceStack/blob/36df1c6390bd85a4dee75b3c141b97a01a916165/src/ServiceStack/AppHostBase.NetCore.cs
    //https://github.com/ServiceStack/ServiceStack/blob/3e10fb024989e4c39bafb3dfaba507129e1749c0/src/ServiceStack.Kestrel/AppSelfHostBase.cs
    // looks like it might have slightly worked once:
    // https://github.com/lotosbin/binbin-dotnetcore-shims/blob/451dd19aa5e2d72e80a95494251fc2f716398983/src/binbin-dotnetcore-shims/System.Web/HttpServerUtility.cs
    // https://github.com/ServiceStack/ServiceStack/blob/36df1c6390bd85a4dee75b3c141b97a01a916165/src/ServiceStack/Host/NetCore/NetCoreRequest.cs
    // https://github.com/aspnet/AspNetWebStack/blob/62d0b2df0bcca848a8e0848fd1866928df15528e/src/System.Web.Mvc/MvcHttpHandler.cs
    public class IISHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://msdn.microsoft.com/en-us/library/46c5ddfy.aspx
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }


        public void ProcessRequest(HttpContext context)
        {
            _server.ProcessRequest(context);
        }


        #endregion

        private readonly IISHandlerServer _server = new IISHandlerServer();
    }
}
