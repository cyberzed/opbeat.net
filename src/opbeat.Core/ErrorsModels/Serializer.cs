using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace opbeat.Core.ErrorsModels
{
    internal class Serializer : IDisposable
    {
        private readonly StringWriter buffer;
        private readonly JsonTextWriter writer;

        public Serializer()
        {
            buffer = new StringWriter();
            writer = new JsonTextWriter(buffer)
                     {
                         DateFormatHandling = DateFormatHandling.IsoDateFormat
                     };

            writer.WriteStartObject();
        }

        public void Dispose()
        {
            buffer.Dispose();
        }

        public string ToJson()
        {
            writer.WriteEndObject();

            return buffer.ToString();
        }

        public void Write<TValue>(string name, TValue value)
        {
            if (value == null)
            {
                return;
            }

            writer.WritePropertyName(name);
            writer.WriteValue(value);
        }

        public void Write<TValue>(string name, TValue[] values)
        {
            if (values == null || values.Length == 0)
            {
                return;
            }

            writer.WritePropertyName(name);

            writer.WriteStartArray();

            foreach (var value in values)
            {
                writer.WriteValue(value);
            }

            writer.WriteEndArray();
        }

        public void Write<TKey, TValue>(string name, IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || !dictionary.Any())
            {
                return;
            }

            writer.WritePropertyName(name);

            foreach (var pair in dictionary)
            {
                writer.WriteStartObject();

                writer.WritePropertyName(pair.Key.ToString());
                writer.WriteValue(pair.Value);

                writer.WriteEndObject();
            }
        }
    }
}