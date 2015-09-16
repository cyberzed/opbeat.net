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

        public string Message { get; }
        public string MessageFormat { get; private set; }
        public DateTime? Timestamp { get; private set; }
        public ErrorLevel? Level { get; private set; }
        public string Logger { get; private set; }
        public string Culprit { get; private set; }
        public Exception Exception { get; private set; }
        public StackTrace StackTrace { get; private set; }

        public IReadOnlyDictionary<string, string> Machine
            => machine.Any() ? new ReadOnlyDictionary<string, string>(machine) : null;

        public IReadOnlyDictionary<string, string> Extra
            => extra.Any() ? new ReadOnlyDictionary<string, string>(extra) : null;

        public Http Http { get; private set; }

        public IReadOnlyDictionary<string, string> User
            => user.Any() ? new ReadOnlyDictionary<string, string>(user) : null;

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
                throw new ArgumentOutOfRangeException(nameof(timestamp));
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

        internal string ToJson()
        {
            using (var serializer = new Serializer())
            {
                serializer.Write("message", Message);
                serializer.Write("timestamp", Timestamp);
                serializer.Write("level", Level);
                serializer.Write("logger", Logger);
                serializer.Write("culprit", Culprit);
                serializer.Write("machine", Machine);
                serializer.Write("extra", Extra);
                serializer.Write("param_message", MessageFormat);

                if (Exception != null)
                {
                    using (var exceptionSerializer = new Serializer())
                    {
                        var exceptionJson = exceptionSerializer.ToJson();
                    }

                    //    writer.WritePropertyName(nameof(x.Baz));
                    //    using (var subbuffer = new StringWriter())
                    //    using (var subwriter = new JsonTextWriter(subbuffer))
                    //    {
                    //        subwriter.WriteStartObject();
                    //        subwriter.WritePropertyName(nameof(x.Baz.Bar));
                    //        subwriter.WriteValue(x.Baz.Bar);
                    //        subwriter.WriteEndObject();

                    //        var json = subbuffer.ToString();

                    //        writer.WriteRawValue(json);
                    //    }
                }

                //serializer.BeginWriteObject("exception");
                //serializer.Write("type", Exception.Type);
                //serializer.Write("value", Exception.Value);
                //serializer.Write("module", Exception.Module);
                //serializer.EndWriteObject();

                //serializer.BeginWriteObject("stacktrace");
                //serializer.Write("stacktrace", StackTrace);
                //serializer.EndWriteObject();

                //serializer.Write("http", Http);
                serializer.Write("user", User);

                return serializer.ToJson();
            }
        }
    }
}