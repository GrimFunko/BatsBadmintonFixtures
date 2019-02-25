using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using System.Net;
using System.Net.Http;

using MvvmHelpers;
using Xamarin.Forms;
using Newtonsoft.Json;

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;



namespace BatsBadmintonFixtures.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand RememberCommand { get; }

        public LoginViewModel()
        {
            Title = "Bats Login";
            LoginCommand = new Command(async () => await CheckLoginAttempt());
            RegisterCommand = new Command(() => Application.Current.MainPage = new RegistrationPage());
            RememberCommand = new Command(async () => await RememberDetailsPressed());

            if(Application.Current.Properties.ContainsKey("loginDetails"))
            {           
                var loginDeets = Application.Current.Properties ["loginDetails"] as Dictionary<string,string>;
                Username = loginDeets["username"];
                Password = loginDeets["password"];
            }
        }

        private bool Remember = false;

        private string _username = "";
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        async Task CheckLoginAttempt()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            bool success = false;

            if (Remember)
                SaveLoginDetails();

            try
            {
                var post = Utilities.GetJsonString(new { type = "login-request", username = Username, password = Password });
                using (var loginResponse =
                    await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/user/login", 
                                                        new StringContent(post,Encoding.UTF8,"application/json")))
                {
                    var json = await loginResponse.Content.ReadAsStringAsync();
                    var response = LoginResponse.FromJson(json);

                    if (response.ApiKey != null)
                    { 
                        success = true;
                        Application.Current.Properties["UserAccessLevel"] = response.AccessLevel;
                        Application.Current.Properties["ApiKey"] = response.ApiKey;
                        Application.Current.Properties["CurrentUserId"] = response.UserID;

                        Utilities.ApiClient.DefaultRequestHeaders.Add("Apikey", response.ApiKey);
                    }

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Something went wrong",
                                    ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            if (success)
            {
                success = false;
                await Application.Current.MainPage.DisplayAlert("Successful login!", $"Welcome, {Username}.", "OK");
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                Application.Current.MainPage = new HomePage();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Login failed.", "Your login details are incorrect.", "OK");

        }

        private void SaveLoginDetails()
        {
            Dictionary<string, string> loginDetails = new Dictionary<string, string>();
            loginDetails.Add("username", Username);
            loginDetails.Add("password", Password);

            Application.Current.Properties.Add("loginDetails", loginDetails);

        }

        private async Task RememberDetailsPressed()
        {
            Remember = true;

            await Application.Current.MainPage.DisplayAlert("Remember login details", "Your details will be stored upon login", "OK");
        }

    }
}
