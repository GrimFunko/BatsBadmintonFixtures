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

        public LoginViewModel()
        {
            Title = "Bats Login";
            LoginCommand = new Command(async () => await CheckLoginAttempt());
        }

        private string _username = "";
        public string Username
        {
            get { return _username; }
            set { SetProperty<string>(ref _username, value, Username,() => OnPropertyChanged(Username)); }
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

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                };
                var client = new HttpClient(clientHandler);
                ConfigurationData cd = new ConfigurationData();
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue(cd.AuthHeaderType, cd.AuthHeaderPassword);
               
                var loginResponse = await client.PostAsync("https://project-api.lukeglasgow.co.uk/",
                                                            new StringContent(GetPOSTJson(Username,Password),Encoding.UTF8, "application/json")
                                                          );
                var json = await loginResponse.Content.ReadAsStringAsync();

                var response = LoginResponse.FromJson(json);
               
                if (response.Valid)
                {
                    await Application.Current.MainPage.DisplayAlert("Successful login!", $"Welcome, {Username}.", "OK");
                    Application.Current.MainPage = new FixturesPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login failed.", "Your login details are incorrect.", "OK");
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

        }

        public string GetPOSTJson(string _Username, string _Password)
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
    }
}
