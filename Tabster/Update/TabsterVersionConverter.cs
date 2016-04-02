#region

using System;
using Newtonsoft.Json;
using Tabster.Core.Types;

#endregion

namespace Tabster.Update
{
    /// <summary>
    ///     Converts a <see cref="TabsterVersion" /> to and from a string (e.g. "1.2.3.4").
    /// </summary>
    public class TabsterVersionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (value is TabsterVersion)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected TabsterVersion object value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                try
                {
                    var v = new TabsterVersion((string) reader.Value);
                    return v;
                }
                catch (Exception)
                {
                }
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (TabsterVersion);
        }
    }
}