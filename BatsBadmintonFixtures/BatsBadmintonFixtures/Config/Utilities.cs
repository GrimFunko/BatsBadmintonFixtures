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
        public static string GetPOSTJson(string _Username, string _Password)
        {
            object bodyObject = new
            {
                type = "login-request",
                username = _Username,
                password = _Password
            };
            string json = JsonConvert.SerializeObject(bodyObject);

            return json;
        }

        public static string GetPOSTJson()
        {
            object bodyObject = new
            {
                type = "fixture-request"
            };
            string json = JsonConvert.SerializeObject(bodyObject);

            return json;
        }

        public static HttpClient GetClient()
        {
            ConfigurationData cd = new ConfigurationData();

            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = cd.BaseAddress;

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(cd.AuthHeaderType,cd.AuthHeaderPassword);

            return client;
        }

    }
}
