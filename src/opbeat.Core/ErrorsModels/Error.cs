using System;
using System.Collections.Generic;

namespace opbeat.Core.ErrorsModels
{
    public class Error
    {
        public string Message { get; private set; }
        public string Param_Message { get; set; }
        public DateTime? Timestamp { get; private set; }
        public ErrorLevel? Level { get; private set; }
        public string Logger { get; set; }
        public string Culprit { get; set; }
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

        public void SetTimestamp(DateTime timestamp)
        {
            if (timestamp == DateTime.MinValue || timestamp == DateTime.MaxValue)
            {
                throw new ArgumentOutOfRangeException("timestamp");
            }

            Timestamp = timestamp;
        }

        public void SetLevel(ErrorLevel level)
        {
            Level = level;
        }

        public void SetLoggerName(string logger)
        {
            if (string.IsNullOrWhiteSpace(logger))
            {
                throw new ArgumentNullException("logger");
            }

            Logger = logger;
        }
    }
}