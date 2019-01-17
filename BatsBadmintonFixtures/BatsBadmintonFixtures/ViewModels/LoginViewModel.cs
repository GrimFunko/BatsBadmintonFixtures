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
            try
            {
                var client = Utilities.GetClient();

                             
                var loginResponse = 
                    await client.PostAsync(client.BaseAddress, new StringContent(Utilities.GetPOSTJson(Username,Password),
                                                                                 Encoding.UTF8, 
                                                                                 "application/json"));
                var json = await loginResponse.Content.ReadAsStringAsync();
                var response = LoginResponse.FromJson(json);

                if (!response.Valid)
                    await Application.Current.MainPage.DisplayAlert("Login failed.", "Your login details are incorrect.", "OK");
                else
                    success = true;

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
                Application.Current.MainPage = new FixturesPage();
            }

        }


    }
}
