using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.ViewModels;

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
            if (!isLoggedIn || !Cache.Contains("ApiKey") || !Cache.Contains("CurrentUser"))
                MainPage = Factory.CreatePage(typeof(LoginPage), typeof(LoginViewModel));
            else
            {
                MainPage = Factory.CreatePage(typeof(HomePage));
                Utilities.ApiClient.DefaultRequestHeaders.Add("Apikey", (string)Cache.Get("ApiKey"));
                AppCurrent.User = Cache.GetUser();
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
