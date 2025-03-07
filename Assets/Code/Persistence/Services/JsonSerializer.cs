using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Systems.Shapes;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Systems.Persistence
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings settings;
        
        public JsonSerializer()
        {
            settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] 
                {
                    new ShapeConverter(),
                }
            };
        }
        
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
    
    public class ShapeConverter : JsonConverter<Shape>
    {
        public override void WriteJson(JsonWriter writer, Shape value, Newtonsoft.Json.JsonSerializer serializer)
        {
            string code = value.ToString();
            
            writer.WriteStartObject();
            writer.WritePropertyName("Shape");
            writer.WriteValue(code);
            writer.WriteEndObject();
        }

        public override Shape ReadJson(JsonReader reader, Type objectType, Shape existingValue, bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            string code = jsonObject["Shape"].ToString();
            
            return code.ToShape();
        }
    }
}