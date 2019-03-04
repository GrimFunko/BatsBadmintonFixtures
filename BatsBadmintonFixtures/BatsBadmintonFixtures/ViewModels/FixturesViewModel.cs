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
using Microsoft.CSharp;

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;

namespace BatsBadmintonFixtures.ViewModels
{
    public class FixturesViewModel : BaseViewModel, ICacheable
    {
        public ObservableRangeCollection<GroupedFixtures> GroupedUpcomingFixtures { get; set; }
        public ICommand GetUpcomingFixturesCommand { get; }
        public ICommand RefreshFeedCommand { get; }

        private object _selectedFixture;

        public object SelectedFixture
        {
            get { return _selectedFixture; }
            set { SetProperty<object>(ref _selectedFixture, value, "SelectedFixture", () => OnPropertyChanged("SelectedFixture")); }
        }


        private bool _isFeedNeeded = true;
        public bool IsFeedNeeded
        {
            get { return _isFeedNeeded; }
            set { SetProperty<bool>(ref _isFeedNeeded, value, "IsFeedNeeded", () => OnPropertyChanged("IsFeedNeeded")); }
        }

        public string CacheItemName { get; set; }

        public FixturesViewModel()
        {
            Title = "Upcoming Fixtures";
            CacheItemName = "Fixture";
            GroupedUpcomingFixtures = new ObservableRangeCollection<GroupedFixtures>();
            if (CacheAvailable())
                DisplayCache();

            GetUpcomingFixturesCommand = new Command(async () => await GetUpcomingFixtures(false));
            RefreshFeedCommand = new Command(async () => await GetUpcomingFixtures(true));

        }

        // TODO Investigate IsRefreshing exhibiting always true behaviour
        async Task GetUpcomingFixtures(bool refresh)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            Fixture[] all;
            string json = null;

            try
            {
                json = await GetAPIData();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Something went wrong!", ex.Message, "OK");
                return;
            }

            if (json == null)
                return;

            all = Fixture.FromJsonArray(json);

            if (all[0].Message != null)
            {
                await Application.Current.MainPage.DisplayAlert("Something went wrong!", all[0].Message, "OK");
                IsBusy = false;
                return;
            }
            SaveToCache(CacheItemName, json);

            GroupedUpcomingFixtures.ReplaceRange(SortIntoDateGroups(all));
            IsFeedNeeded = CheckForFeedNeeded();
            IsBusy = false;
        }

        public void DisplayCache()
        {
            IsBusy = true;

            var json = GetCache(CacheItemName);
            var fixtures = Fixture.FromJsonArray(json);
            GroupedUpcomingFixtures.ReplaceRange(SortIntoDateGroups(RemoveOldFixtures(fixtures)));

            IsFeedNeeded = CheckForFeedNeeded();
            IsBusy = false;
        }

        public List<string> GetListOfDistinctDates(Fixture[] fixtures)
        {
            List<string> distinctDates = new List<string>();
            string date = "";
            foreach (Fixture fixture in fixtures)
            {
                date = fixture.Date;
                if (distinctDates.Contains(date))
                    continue;
                else distinctDates.Add(date);
            }

            return distinctDates;
        }

        public bool CheckForFeedNeeded()
        {
            return GroupedUpcomingFixtures.Count > 0 ? false : true;
        }
          
        public List<GroupedFixtures> SortIntoDateGroups(Fixture[] fixtures)
        {
            List<GroupedFixtures> groupedList = new List<GroupedFixtures>();
            var Jan = new GroupedFixtures() { LongName = "January", ShortName = "Jan" };
            var Feb = new GroupedFixtures() { LongName = "February", ShortName = "Feb" };
            var Mar = new GroupedFixtures() { LongName = "March", ShortName = "Mar" };
            var Apr = new GroupedFixtures() { LongName = "April", ShortName = "Apr" };
            var May = new GroupedFixtures() { LongName = "May", ShortName = "May" };
            var Jun = new GroupedFixtures() { LongName = "June", ShortName = "Jun" };
            var Jul = new GroupedFixtures() { LongName = "July", ShortName = "Jul" };
            var Aug = new GroupedFixtures() { LongName = "August", ShortName = "Aug" };
            var Sep = new GroupedFixtures() { LongName = "Spetember", ShortName = "Sep" };
            var Oct = new GroupedFixtures() { LongName = "October", ShortName = "Oct" };
            var Nov = new GroupedFixtures() { LongName = "November", ShortName = "Nov" };
            var Dec = new GroupedFixtures() { LongName = "December", ShortName = "Dec" };

            for (int i = 0; i < fixtures.Length; i++)
            {
                if (fixtures[i].Date.Contains("-01-"))
                {
                    Jan.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-02-"))
                {
                    Feb.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-03-"))
                {
                    Mar.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-04-"))
                {
                    Apr.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-05-"))
                {
                    May.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-06-"))
                {
                    Jun.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-07-"))
                {
                    Jul.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-08-"))
                {
                    Aug.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-09-"))
                {
                    Sep.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-10-"))
                {
                    Oct.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-11-"))
                {
                    Nov.Add(fixtures[i]);
                    continue;
                }
                if (fixtures[i].Date.Contains("-12-"))
                {
                    Dec.Add(fixtures[i]);
                    continue;
                }
            }

            if (Jan.Count > 0)
                groupedList.Add(Jan);
            if (Feb.Count > 0)
                groupedList.Add(Feb);                
            if (Mar.Count > 0)
                groupedList.Add(Mar);
            if (Apr.Count > 0)
                groupedList.Add(Apr);
            if (May.Count > 0)
                groupedList.Add(May);
            if (Jun.Count > 0)
                groupedList.Add(Jun);
            if (Jul.Count > 0)
                groupedList.Add(Jul);
            if (Aug.Count > 0)
                groupedList.Add(Aug);
            if (Sep.Count > 0)
                groupedList.Add(Sep);
            if (Oct.Count > 0)
                groupedList.Add(Oct);
            if (Nov.Count > 0)
                groupedList.Add(Nov);
            if (Dec.Count > 0)
                groupedList.Add(Dec);

            return groupedList;
        }

        public void SaveToCache(string cacheName, string jsonItem)
        {  
            Cache.Save(cacheName, jsonItem);
            Cache.Save("FixtureCacheDate", DateTime.Now.Date);
        }

        public string GetCache(string cacheName)
        {
            return (string) Cache.Get(cacheName);
        }

        private async Task<string> GetAPIData()
        { 
            using (var fixturesResponse =
                await Utilities.ApiClient.GetAsync(Utilities.ApiClient.BaseAddress + "/fixtures/"))
            {
                return await fixturesResponse.Content.ReadAsStringAsync();
            }
        }

        public Fixture[] RemoveOldFixtures(Fixture[] fixArray)
        {
            var fixList = fixArray.ToList();
            var date = DateTime.Now.Date;
            foreach (Fixture fix in fixArray)
            {
                var fixDate = Convert.ToDateTime(fix.Date);
                if (fixDate < date)
                    fixList.Remove(fix);
            }

            return fixList.ToArray();
        }

        public bool CacheAvailable()
        {
            // check if cache is there and check the date, return bool satisfying availability
            bool exists = Cache.Contains(CacheItemName);
            bool inDate = Cache.Contains("FixtureCacheDate") && 
                (Convert.ToDateTime(Cache.Get("FixtureCacheDate")) >= DateTime.Now.AddDays(-7));

            if (exists && inDate)
                return true;

            return false;
        }
    }
}

