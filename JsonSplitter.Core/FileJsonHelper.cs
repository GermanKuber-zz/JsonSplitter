using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonSplitter.Core
{
    public static class FileJsonHelper
    {
        public static void SerializeSequenceToJsonList<T>(this IEnumerable<T> sequence, string fileName)
        {
            using (var fileStream = File.CreateText(fileName))
                SerializeSequenceToJson(sequence, fileStream);
        }
        public static void SerializeSequenceToJson<T>(this T sequence, string fileName)
        {
            using (var fileStream = File.CreateText(fileName))
                SerializeSequenceToJson(sequence, fileStream);
        }

        public static IEnumerable<T> DeserializeSequenceFromJson<T>(string fileName)
        {
            using (var fileStream = File.OpenText(fileName))
                foreach (var responseJson in DeserializeSequenceFromJson<T>(fileStream))
                    yield return responseJson;
        }

        public static void SerializeSequenceToJson<T>(this IEnumerable<T> sequence, TextWriter writeStream, Action<T, long> progress = null)
        {
            using (var writer = new JsonTextWriter(writeStream))
            {
                var serializer = new JsonSerializer();
                writer.WriteStartArray();
                long index = 0;
                foreach (var item in sequence)
                {
                    if (progress != null)
                        progress(item, index++);

                    serializer.Serialize(writer, item);
                }
                writer.WriteEnd();
            }
        }
        public static void SerializeSequenceToJson<T>(this T item, TextWriter writeStream, Action<T, long> progress = null)
        {
            using (var writer = new JsonTextWriter(writeStream))
            {

                var serializer = new JsonSerializer();
                long index = 0;
                serializer.Serialize(writer, item);
            }
        }
        public static IEnumerable<T> DeserializeSequenceFromJson<T>(TextReader readerStream)
        {
            using (var reader = new JsonTextReader(readerStream))
            {
                var serializer = new JsonSerializer();
                if (!reader.Read() || reader.TokenType != JsonToken.StartArray)
                    throw new Exception("Expected start of array in the deserialized json string");

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndArray) break;
                    var item = serializer.Deserialize<T>(reader);
                    yield return item;
                }
            }
        }
    }
}
