using System;
using Newtonsoft.Json;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// Converts author resource to string using it's name
    /// </summary>
    public class AuthorConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        public override void WriteJson(JsonWriter writer, [AllowNull] object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, [AllowNull] object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                while ((reader.TokenType == JsonToken.PropertyName && Equals(reader.Value, "sch:name")) == false)
                {
                    reader.Read();
                }

                reader.Read();
                var authorName = reader.Value;
                reader.Read();
                return authorName;
            }

            throw new InvalidOperationException("Cannot deserialize author - must be a JObject");
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
