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
    public class FixturesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<GroupedFixtures> GroupedUpcomingFixtures { get; set; }
        public ICommand GetUpcomingFixturesCommand { get; }

        private bool _isFeedNeeded = true;
        public bool IsFeedNeeded
        {
            get { return _isFeedNeeded; }
            set { SetProperty<bool>(ref _isFeedNeeded, value, "IsFeedNeeded", () => OnPropertyChanged("IsFeedNeeded")); }
        }

        public FixturesViewModel()
        {
            Title = "Fixtures";
            GroupedUpcomingFixtures = new ObservableRangeCollection<GroupedFixtures>();

            GetUpcomingFixturesCommand = new Command(async () => await GetUpcomingFixtures());
        }

        async Task GetUpcomingFixtures()
        {
            if (IsBusy) 
                return;
            IsBusy = true;
            
            try
            {
                var client = Utilities.GetClient();
                var fixturesResponse = await client.PostAsync(client.BaseAddress, new StringContent(Utilities.GetPOSTJson(),
                                                                                                    Encoding.UTF8,
                                                                                                    "application/json"));
                var json = await fixturesResponse.Content.ReadAsStringAsync();

                var all = Fixture.FromJson(json);

                GroupedUpcomingFixtures.ReplaceRange(SortIntoDateGroups(all));
                IsFeedNeeded = false;
            }
            catch(Exception Ex)
            {
                await Application.Current.MainPage.DisplayAlert("Something went wrong!", Ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
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
            List<GroupedFixtures> groupedList = AddFixturesToDateGroup(fixtures);
            
            return groupedList;
        }

        public List<GroupedFixtures> AddFixturesToDateGroup(Fixture[] fixtures)
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


    }
}

