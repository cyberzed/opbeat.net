using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

namespace opbeat.Core.ErrorsModels
{
    public class Http
    {
        private readonly IDictionary<string, string> headers;
        public Uri Url { get; private set; }
        public HttpMethod Method { get; private set; }
        public string RequestBody { get; private set; }
        public string QueryString { get; private set; }
        public string Cookies { get; private set; }

        public IReadOnlyDictionary<string, string> Headers
        {
            get { return headers.Any() ? new ReadOnlyDictionary<string, string>(headers) : null; }
        }

        public string RemoteHost { get; set; }
        public string HttpHost { get; set; }
        public string UserAgent { get; set; }
        public bool Secure { get; set; }
        public Dictionary<string, string> Environment { get; set; }

        public Http(Uri url, HttpMethod method)
        {
            Url = url;
            Method = method;

            headers = new Dictionary<string, string>();
        }

        public void SetRequestBody(string body)
        {
            RequestBody = body;
        }

        public void SetQueryString(string queryString)
        {
            QueryString = queryString;
        }
    }
}