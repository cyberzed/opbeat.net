using System;
using System.Collections.Generic;
using System.Text;
using opbeat.Core.ErrorsModels;
using opbeat.Core.ReleaseModels;

namespace opbeat.Core
{
    public static class Serializer
    {
        private static readonly Dictionary<Type, string> Formatters = new Dictionary<Type, string>
        {
            {typeof (string), "\"{0}\":\"{1}\","},
            {typeof (DateTime?), "\"{0}\":\"{1:o}\","},
            {typeof (Dictionary<string, string>), "\"{0}\": {1},"}
        };

        private static readonly Dictionary<ErrorLevel, string> ErrorLevelMap = new Dictionary<ErrorLevel, string>
        {
            {ErrorLevel.Debug, "debug"},
            {ErrorLevel.Error, "error"},
            {ErrorLevel.Fatal, "fatal"},
            {ErrorLevel.Info, "info"},
            {ErrorLevel.Warning, "warning"}
        };

        private static readonly Dictionary<ReleaseStatus, string> ReleaseStatusMap = new Dictionary<ReleaseStatus, string>
        {
            {ReleaseStatus.Completed, "completed"},
            {ReleaseStatus.MachineCompleted, "machine-completed"}
        };

        public static string Serialize(Release release)
        {
            var buffer = new StringBuilder(128);

            buffer.Append("rev", release.CommitHash);
            buffer.Append("status", ReleaseStatusMap[release.Status]);
            buffer.Append("branch", release.Branch);

            if (release.Status == ReleaseStatus.MachineCompleted)
            {
                buffer.Append("machine", release.MachineName);
            }

            FinalizeSerialization(buffer);

            return buffer.ToString();
        }

        public static string Serialize(Error error)
        {
            var buffer = new StringBuilder(512);

            buffer.Append("message", error.Message);

            buffer.Append("timestamp", error.Timestamp);

            if (error.Level.HasValue)
            {
                buffer.Append("level", ErrorLevelMap[error.Level.Value]);
            }

            buffer.Append("logger", error.Logger);
            buffer.Append("culprint", error.Culprint);
            buffer.Append("machine", error.Machine);
            buffer.Append("extra", error.Extra);

            FinalizeSerialization(buffer);

            return buffer.ToString();
        }

        private static void Append<TType>(this StringBuilder buffer, string name, TType value)
        {
            var formatter = Formatters[typeof (TType)];

            if (typeof (TType) == typeof (Dictionary<string, string>))
            {
                var dictionaryBuffer = new StringBuilder(128);

                var subValues = value as Dictionary<string, string>;

                if (subValues != null)
                {
                    foreach (var subValue in subValues)
                    {
                        dictionaryBuffer.Append(subValue.Key, subValue.Value);
                    }

                    FinalizeSerialization(dictionaryBuffer);

                    buffer.AppendFormat(formatter, name, dictionaryBuffer);
                }

                return;
            }

            if (!IsNull(value))
            {
                buffer.AppendFormat(formatter, name, value);
            }
        }

        private static bool IsNull<TType>(TType value)
        {
            if (typeof (TType) == typeof (string))
            {
                return string.IsNullOrWhiteSpace(value as string);
            }

            return value == null || value.Equals(default(TType));
        }

        private static void FinalizeSerialization(StringBuilder buffer)
        {
            buffer.Remove(buffer.Length - 1, 1);
            buffer.Insert(0, "{");
            buffer.Append("}");
        }
    }
}