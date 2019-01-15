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
        public ObservableRangeCollection<Fixture> GroupedUpcomingFixtures { get; }
        public ICommand GetUpcomingFixturesCommand { get; }

        public FixturesViewModel()
        {
            Title = "Fixtures";
            GroupedUpcomingFixtures = new ObservableRangeCollection<Fixture>();
            GetUpcomingFixturesCommand = new Command(async () => await GetUpcomingFixtures());
        }

        async Task GetUpcomingFixtures()
        {
            if (IsBusy) 
                return;
            IsBusy = true;
            

            await Application.Current.MainPage.DisplayAlert("Hello!", "This is a message.", "OK");

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
    }
}
