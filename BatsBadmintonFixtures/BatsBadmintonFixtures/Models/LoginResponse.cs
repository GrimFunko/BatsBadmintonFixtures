using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class LoginResponse
    {
        [JsonProperty("user_id")]
        public string UserID { get; set; }

        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }

        [JsonProperty("key")]
        public string ApiKey { get; set; }
    }

    public partial class LoginResponse
    {
        public static LoginResponse FromJson(string json) => JsonConvert.DeserializeObject<LoginResponse>(json);
    }
}
