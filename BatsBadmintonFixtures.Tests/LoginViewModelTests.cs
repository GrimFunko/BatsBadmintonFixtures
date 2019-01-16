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
            string username = "appadmin";
            string password = "adminpassword";            

            string expected = "{\"type\":\"login-request\",\"username\":\"appadmin\"," +
                                                                          "\"password\":\"adminpassword\"}";

            // Actual
            string actual = Utilities.GetPOSTJson(username, password);
            // Assert

            Assert.Equal(expected, actual);
        }

    }
}
