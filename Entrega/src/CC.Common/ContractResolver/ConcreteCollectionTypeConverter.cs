using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Common.Helpers.Contract
{
    public class ConcreteCollectionTypeConverter<TCollection, TItem, TBaseItem> : JsonConverter
    where TCollection : ICollection<TBaseItem>, new()
    where TItem : TBaseItem
    {
        public override void WriteJson(
            JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var collection = new TCollection();

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new InterfaceContractResolver(typeof(TBaseItem)),
            };
            var json = JsonConvert.SerializeObject(serializer.Deserialize<IEnumerable<dynamic>>(reader));
            IEnumerable<TItem> items = JsonConvert.DeserializeObject<IEnumerable<TItem>>(json, jsonSettings);

            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ICollection<TBaseItem>).IsAssignableFrom(objectType);
        }
    }
}
