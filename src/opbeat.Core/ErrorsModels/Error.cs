using System;
using System.Collections.Generic;

namespace opbeat.Core.ErrorsModels
{
    public class Error
    {
        public string Message { get; private set; }
        public string Param_Message { get; set; }
        public DateTime? Timestamp { get; set; }
        public ErrorLevel? Level { get; set; }
        public string Logger { get; set; }
        public string Culprint { get; set; }
        public Exception Exception { get; set; }
        public StackTrace StackTrace { get; set; }
        public Dictionary<string, string> Machine { get; set; }
        public Dictionary<string, string> Extra { get; set; }
        public Http Http { get; set; }
        public Dictionary<string, string> User { get; set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}