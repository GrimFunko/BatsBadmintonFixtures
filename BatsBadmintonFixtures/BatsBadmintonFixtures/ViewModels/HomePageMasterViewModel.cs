﻿using System;
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
        public ObservableRangeCollection<HomePageMenuItem> MenuItems { get; set; }

        public HomePageMasterViewModel()
        {
            LogoutCommand = new Command(() => Logout());
            MenuItems = new ObservableRangeCollection<HomePageMenuItem>();
            PopulateMenuItems();
        }

        private void PopulateMenuItems()
        {
            MenuItems.Add(new HomePageMenuItem { Title = "Upcoming Fixtures", TargetType = typeof(FixturesPage), ViewModelType = typeof(FixturesViewModel) });
        }

        // TODO Clear cache on logout
        public void Logout()
        {
            Cache.RemoveAll();
            Cache.Save("IsLoggedIn", false);
            Application.Current.MainPage = Factory.CreatePage(typeof(LoginPage), typeof(LoginViewModel));
        }
    }
}
