using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json;
using BatsBadmintonFixtures.Models;

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

    public static class AppCurrent
    {
        public static User User { get; set; }


        
    }

    public static class Factory
    {
        public static User CreateUser(AccessLevels access)
        {
            switch (access)
            {
                case AccessLevels.unauthorised:
                    return null;
                case AccessLevels.guest:
                    return new User();
                case AccessLevels.player:
                    return new Player();
                case AccessLevels.captain:
                    return new Captain();
                case AccessLevels.admin:
                    return new Admin();
                default:
                    return null;
            }
        }
    }
}
