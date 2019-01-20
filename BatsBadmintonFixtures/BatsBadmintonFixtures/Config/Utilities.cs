using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Config
{
    //TODO Change HttpClient to static property with initialise function called at application start
    //TODO Create body objects in configuration data for git ignoring
    public static class Utilities
    {
        public static HttpClient ApiClient { get; set; }

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
                type = "fixtures-request"
            };
            string json = JsonConvert.SerializeObject(bodyObject);

            return json;
        }

        public static void InitialiseClient()
        {
            ConfigurationData cd = new ConfigurationData();

            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            ApiClient = new HttpClient(clientHandler);
            ApiClient.BaseAddress = cd.BaseAddress;

            ApiClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(cd.AuthHeaderType,cd.AuthHeaderPassword);
            
        }

    }
}
