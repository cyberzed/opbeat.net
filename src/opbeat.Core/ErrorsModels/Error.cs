using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace opbeat.Core.ErrorsModels
{
    public class Error
    {
        private readonly IDictionary<string, string> extra;
        private readonly IDictionary<string, string> machine;
        private readonly IDictionary<string, string> user;
        public string Message { get; private set; }
        public string MessageFormat { get; private set; }
        public DateTime? Timestamp { get; private set; }
        public ErrorLevel? Level { get; private set; }
        public string Logger { get; private set; }
        public string Culprit { get; private set; }
        public Exception Exception { get; private set; }
        public StackTrace StackTrace { get; private set; }

        public IReadOnlyDictionary<string, string> Machine
        {
            get { return machine.Any() ? new ReadOnlyDictionary<string, string>(machine) : null; }
        }

        public IReadOnlyDictionary<string, string> Extra
        {
            get { return extra.Any() ? new ReadOnlyDictionary<string, string>(extra) : null; }
        }

        public Http Http { get; private set; }

        public IReadOnlyDictionary<string, string> User
        {
            get { return user.Any() ? new ReadOnlyDictionary<string, string>(user) : null; }
        }

        public Error(string message)
        {
            Message = message;

            machine = new Dictionary<string, string>();
            extra = new Dictionary<string, string>();
            user = new Dictionary<string, string>();
        }

        public void SetMessageFormat(string messageFormat)
        {
            MessageFormat = messageFormat;
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
            Logger = logger;
        }

        public void SetCulprint(string culprint)
        {
            Culprit = culprint;
        }

        public void AddException(Exception exception)
        {
            Exception = exception;
        }

        public void AddStrackTrace(StackTrace stackTrace)
        {
            StackTrace = stackTrace;
        }

        public void AddMachineInformation(string key, string value)
        {
            if (machine.ContainsKey(key))
            {
                machine.Remove(key);
            }

            machine.Add(key, value);
        }

        public void AddExtraInformation(string key, string value)
        {
            if (extra.ContainsKey(key))
            {
                extra.Remove(key);
            }

            extra.Add(key, value);
        }

        public void AddHttp(Http http)
        {
            Http = http;
        }

        public void AddUserInformation(string key, string value)
        {
            if (user.ContainsKey(key))
            {
                user.Remove(key);
            }

            user.Add(key, value);
        }
    }
}