using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class Team
    {
        [JsonProperty("team_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TeamId { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public string Captain { get; set; }

        public string FullName { get { return $"{ Name }, { League }"; } }

    }

    public partial class Team
    {
        public static Team[] FromJson(string json) => JsonConvert.DeserializeObject<Team[]>(json);
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
