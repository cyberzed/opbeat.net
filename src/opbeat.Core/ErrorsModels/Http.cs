using System;
using System.Collections.Generic;
using System.Net.Http;

namespace opbeat.Core.ErrorsModels
{
    public class Http
    {
        public Uri Url { get; set; }
        public HttpMethod Method { get; set; }

        //data = dictionary<string,string> or string

        public string Query_String { get; set; }
        public string Cookies { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string Remote_Host { get; set; }
        public string Http_Host { get; set; }
        public string User_Agent { get; set; }

        public bool Secure { get; set; }

        public Dictionary<string, string> Env { get; set; }
    }
}