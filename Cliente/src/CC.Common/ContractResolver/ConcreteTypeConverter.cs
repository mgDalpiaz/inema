using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Common.Helpers.Contract
{
    public class ConcreteTypeConverter<TConcrete, TInterface> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //assume we can convert to anything for now
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            TInterface deserialized = (TInterface)Activator.CreateInstance(typeof(TConcrete));

            serializer.ContractResolver = new InterfaceContractResolver(typeof(TInterface));

            serializer.Populate(jsonObject.CreateReader(), deserialized);
            return deserialized;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine

            serializer.ContractResolver = new InterfaceContractResolver(typeof(TInterface));

            serializer.Serialize(writer, value);
        }
    }
}
