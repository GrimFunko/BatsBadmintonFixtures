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
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
