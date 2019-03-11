using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Models
{
    public partial class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("tel1")]
        public string TelephoneNumber1 { get; set; }

        [JsonProperty("tel2")]
        public string TelephoneNumber2 { get; set; }

        [JsonProperty("fbLink")]
        public string FacebookLink { get; set; }
    }

    public partial class User
    {
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("cpassword")]
        public string ConfirmPassword { get; set; }
    }

    public partial class User
    {
        // Registration constructor
        public User(string _username, string _email, string _password, string _cpassword, string _fName, string _surname,
            string _telephone1, string _telephone2, string _fbLink)
        {
            Username = _username;
            Email = _email;
            Password = _password;
            ConfirmPassword = _cpassword;
            FirstName = _fName;
            Surname = _surname;
            TelephoneNumber1 = _telephone1;
            TelephoneNumber2 = _telephone2;
            FacebookLink = _fbLink;
        }

        // General constructor
        public User() { }
    }

    public partial class User
    {
        public static User FromJson(string json) => JsonConvert.DeserializeObject<User>(json);
    }



}
