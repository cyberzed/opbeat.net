using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace opbeat.Core.ErrorsModels
{
    public class Http
    {
        private readonly IDictionary<string, string> environment;
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

        public IPAddress RemoteHost { get; private set; }

        public string HttpHost
        {
            get { return Url.Host; }
        }

        public string UserAgent { get; private set; }

        public bool Secure
        {
            get { return Url.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase); }
        }

        public IReadOnlyDictionary<string, string> Environment
        {
            get { return environment.Any() ? new ReadOnlyDictionary<string, string>(environment) : null; }
        }

        public Http(Uri url, HttpMethod method)
        {
            if (!url.IsAbsoluteUri)
            {
                throw new ArgumentException("Url must be absolute", "url");
            }

            Url = url;
            Method = method;

            headers = new Dictionary<string, string>();
            environment = new Dictionary<string, string>();
        }

        public void SetRequestBody(string body)
        {
            RequestBody = body;
        }

        public void SetQueryString(string queryString)
        {
            QueryString = queryString;
        }

        public void SetCookies(string cookies)
        {
            Cookies = cookies;
        }

        public void AddHeader(string key, string value)
        {
            if (headers.ContainsKey(key))
            {
                headers.Remove(key);
            }

            headers.Add(key, value);
        }

        public void AddRemoteHost(IPAddress address)
        {
            RemoteHost = address;
        }

        public void AddUserAgent(string userAgent)
        {
            UserAgent = userAgent;
        }

        public void AddEnvironment(string key, string value)
        {
            if (environment.ContainsKey(key))
            {
                environment.Remove(key);
            }

            environment.Add(key, value);
        }
    }
}