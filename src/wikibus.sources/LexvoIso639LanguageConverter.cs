using System;
using System.Globalization;
using Newtonsoft.Json;
using NullGuard;

namespace wikibus.sources
{
    /// <summary>
    /// Convert Lexvo ISO two-letter language Uri to <see cref="Language"/>
    /// </summary>
    public class LexvoIso639LanguageConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        public override void WriteJson(JsonWriter writer, [AllowNull] object value, JsonSerializer serializer)
        {
            var lang = (Language)value;
            writer.WriteValue("langIso:" + lang.Name);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, [AllowNull] object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                while ((reader.TokenType == JsonToken.PropertyName && Equals(reader.Value, "@id")) == false)
                {
                    reader.Read();
                }

                var language = new Language(GetLangName(reader.ReadAsString()));
                reader.Read();
                return language;
            }

            if (reader.ValueType == typeof(string))
            {
                return new Language(GetLangName((string)reader.Value));
            }

            throw new InvalidOperationException(string.Format("Cannot deserialize value {0} as CultureInfo", reader.Value));
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CultureInfo);
        }

        private static string GetLangName(string value)
        {
            return value.Replace("langIso:", string.Empty);
        }
    }
}
