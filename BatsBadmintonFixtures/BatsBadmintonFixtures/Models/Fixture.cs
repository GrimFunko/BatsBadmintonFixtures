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
        public int ID { get; set; }

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

        public string FixtureTeams { get { return Venue + " vs " + TeamVs; } }

        public string MyDate { get { return FormatDateProp(Date); } }

    }

    public partial class Fixture
    { 
        public static Fixture[] FromJson(string json) => JsonConvert.DeserializeObject<Fixture[]>(json);     
        
        private string FormatDateProp(string date)
        {
            char[] splitPat = new char[] { '-' };
            string[] dateComponents = date.Split(splitPat);
            string myDate = $"{dateComponents[2]}/{dateComponents[1]}";
            return myDate;
        }
    }

    public class GroupedFixtures : ObservableRangeCollection<Fixture>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
