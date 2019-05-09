using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class Fixture
    {
        [JsonProperty("fixture_id")]
        public string FixtureId { get; set; }

        [JsonProperty("bats_team")]
        public Team BatsTeam { get; set; }

        [JsonProperty("team_vs")]
        public string TeamVs { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(DateStringConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("time")]
        [JsonConverter(typeof(TimeStringConverter))]
        public TimeSpan Time { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public string FixtureTeams { get { return Venue + " vs " + TeamVs; } }

        //public string FixtureListDateFormat { get { return FormatDateProp(Date); } }

        //public bool FullTeam { get
        //    {
        //        if (BatsTeam.Contains("Combination") || BatsTeam.Contains("Women"))
        //            return false;
        //        return true;
        //    } }

    }

    public partial class Fixture
    { 
        public static Fixture[] FromJsonArray(string json) => JsonConvert.DeserializeObject<Fixture[]>(json);

        public static Fixture FromJson(string json) => JsonConvert.DeserializeObject<Fixture>(json);
    }

    internal class DateStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DateTime) || t == typeof(DateTime?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            DateTime dt;
            if (DateTime.TryParse(value, out dt))
            {
                return dt;
            }
            throw new Exception("Cannot unmarshal type DateTime");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DateTime)untypedValue;
            serializer.Serialize(writer, value.ToString("yyyy-MM-dd"));
            return;
        }

        public static readonly DateStringConverter Singleton = new DateStringConverter();
    }

    internal class TimeStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TimeSpan) || t == typeof(TimeSpan?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            TimeSpan ts;
            if (TimeSpan.TryParse(value, out ts))
            {
                return ts;
            }
            throw new Exception("Cannot unmarshal type TimeSpan");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TimeSpan)untypedValue;
            serializer.Serialize(writer, value.ToString(@"hh\:mm"));
            return;
        }

        public static readonly TimeStringConverter Singleton = new TimeStringConverter();
    }

    public class GroupedFixtures : ObservableRangeCollection<Fixture>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
