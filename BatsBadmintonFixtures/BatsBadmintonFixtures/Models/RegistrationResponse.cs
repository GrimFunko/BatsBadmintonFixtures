using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BatsBadmintonFixtures.Models
{
    public partial class ServerResponseMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public partial class ServerResponseMessage
    {
        public static ServerResponseMessage FromJson(string json) => JsonConvert.DeserializeObject<ServerResponseMessage>(json);
    }
}

