using System;
using Newtonsoft.Json;
using Extension.Primitives;

namespace CC.Extension.Json
{
    public class IntegerJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(int) || objectType == typeof(Int32));
        }

        public override object ReadJson(JsonReader reader, Type objectType,
                                        object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                if (string.IsNullOrWhiteSpace((string)reader.Value))
                {
                    return int.MinValue;
                }
                return ((string)reader.Value).ToInt() ?? 0;
            }
            else if (reader.TokenType == JsonToken.Float ||
                     reader.TokenType == JsonToken.Integer)
            {
                return Convert.ToInt32(reader.Value);
            }

            throw new JsonSerializationException("Unexpected token type: " +
                                                 reader.TokenType.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value,
                                       JsonSerializer serializer)
        {
            int dec = (int)value;
            if (dec == int.MinValue)
            {
                writer.WriteValue(string.Empty);
            }
            else
            {
                writer.WriteValue(dec);
            }
        }
    }
}
