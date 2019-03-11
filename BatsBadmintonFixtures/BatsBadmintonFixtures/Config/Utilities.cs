using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Config
{
    public static class Utilities
    {
        public static HttpClient ApiClient { get; set; }

        public static string GetJsonString(object obj) => JsonConvert.SerializeObject(obj);

        public static void InitialiseClient()
        {
            ConfigurationData cd = new ConfigurationData();

            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };
            ApiClient = new HttpClient(clientHandler);
            ApiClient.BaseAddress = cd.BaseAddress;
            
        }
    }

    public enum AccessLevels
    {
        unauthorised = -1,
        guest = 0,
        player = 1,
        captain = 2,
        admin = 3
    };

    public static class CurrentUser
    {
        public static AccessLevels AccessLevel { get; set; }

        public static string Username { get; set; }

        public static string FirstName { get; set; }

        public static string UserId { get; set; }
    }
}
