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

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;

namespace BatsBadmintonFixtures.ViewModels
{
    class HomePageMasterViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; }
        
        public HomePageMasterViewModel()
        {
            LogoutCommand = new Command(() => Logout());
        }


        public void Logout()
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.MainPage = new LoginPage();
        }
    }
}
