using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class LoginResponse
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("access level")]
        public string AccessLevel { get; set; }

    }

    public partial class LoginResponse
    {
        public static LoginResponse FromJson(string json) => JsonConvert.DeserializeObject<LoginResponse>(json);
    }
}
