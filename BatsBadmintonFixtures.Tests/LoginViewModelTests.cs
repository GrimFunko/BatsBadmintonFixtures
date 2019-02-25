using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Config;
using Xamarin.Forms;
using Xunit;
using Newtonsoft.Json;

namespace BatsBadmintonFixtures.Tests
{
    public class LoginViewModelTests
    {
		[Fact]
		public void GetPostJson_ShouldReturnCorectlyFormattedString()
        {
            // Arrange
            string _username = "appadmin";
            string _password = "adminpassword";            

            string expected = "{\"type\":\"login-request\",\"username\":\"appadmin\"," +
                                                                          "\"password\":\"adminpassword\"}";

            object obj = new { type = "login-request", username = _username, password = _password };

            // Actual
            string actual = Utilities.GetJsonString(obj);
            // Assert

            Assert.Equal(expected, actual);
        }

    }
}
