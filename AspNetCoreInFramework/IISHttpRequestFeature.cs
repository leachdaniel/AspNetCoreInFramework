using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AspNetCoreInFramework
{
    public class IISHttpRequestFeature : IHttpRequestFeature
    {
        public string Protocol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Scheme { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Method { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PathBase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string QueryString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RawTarget { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IHeaderDictionary Headers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Stream Body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}