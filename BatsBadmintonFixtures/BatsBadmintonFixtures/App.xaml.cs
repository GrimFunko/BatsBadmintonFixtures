using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BatsBadmintonFixtures.Config;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BatsBadmintonFixtures
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Utilities.InitialiseClient();

            bool isLoggedIn = Cache.Contains("IsLoggedIn") ? (bool)Cache.Get("IsLoggedIn") : false;
            if (!isLoggedIn)
                MainPage = new LoginPage();
            else
            {
                MainPage = new HomePage();
                if(Cache.Contains("ApiKey"))
                    Utilities.ApiClient.DefaultRequestHeaders.Add("Apikey", (string)Cache.Get("ApiKey"));
                if (Cache.Contains("UserAccessLevel"))
                    CurrentUser.AccessLevel = (AccessLevels)Cache.Get("UserAccessLevel");
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
