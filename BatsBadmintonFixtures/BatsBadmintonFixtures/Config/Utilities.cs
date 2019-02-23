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

        public static string GetJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static void InitialiseClient()
        {
            ConfigurationData cd = new ConfigurationData();

            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            ApiClient = new HttpClient(clientHandler);
            ApiClient.BaseAddress = cd.BaseAddress;
            
        }

    }
}
