using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class Fixture
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        [JsonProperty("bats_team")]
        public string BatsTeam { get; set; }

        [JsonProperty("team_vs")]
        public string TeamVs { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public string FixtureTeams { get { return Venue + " vs " + TeamVs; } }

        public string FixtureListDateFormat { get { return FormatDateProp(Date); } }

        public bool FullTeam { get
            {
                if (BatsTeam.Contains("Combination") || BatsTeam.Contains("Women"))
                    return false;
                return true;
            } }

    }

    public partial class Fixture
    { 
        public static Fixture[] FromJsonArray(string json) => JsonConvert.DeserializeObject<Fixture[]>(json);

        public static Fixture FromJson(string json) => JsonConvert.DeserializeObject<Fixture>(json);
        
        private string FormatDateProp(string date)
        {             
            string[] dateComponents = date.Split(new char[] { '-' });
            DateTime dt = new DateTime(Convert.ToInt16(dateComponents[0]), Convert.ToInt16(dateComponents[1]), Convert.ToInt16(dateComponents[2]));
            return $"{dt.DayOfWeek} {dateComponents[2]}/{dateComponents[1]}";
        }
    }

    public class GroupedFixtures : ObservableRangeCollection<Fixture>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
