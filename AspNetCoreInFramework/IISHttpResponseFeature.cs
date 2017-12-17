using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreInFramework
{
    public class IISHttpResponseFeature : IHttpResponseFeature
    {
        public int StatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ReasonPhrase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IHeaderDictionary Headers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Stream Body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool HasStarted => throw new NotImplementedException();

        public void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }
    }
}