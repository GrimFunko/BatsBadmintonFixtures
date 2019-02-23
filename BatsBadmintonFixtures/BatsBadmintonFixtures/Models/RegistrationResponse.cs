using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BatsBadmintonFixtures.Models
{
    public partial class RegistrationResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public partial class RegistrationResponse
    {
        public static RegistrationResponse FromJson(string json) => JsonConvert.DeserializeObject<RegistrationResponse>(json);
    }
}

