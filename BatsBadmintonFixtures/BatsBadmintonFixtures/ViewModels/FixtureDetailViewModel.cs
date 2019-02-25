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
    public class FixtureDetailViewModel : BaseViewModel
    {
        public bool AdminPrivilige { get
            {
                if ((string)Application.Current.Properties["UserAccessLevel"] == "admin")
                    return true;
                return false;
            }
        }
        public ICommand OpenEditCommand { get; set; }

        public FixtureDetailViewModel(object fixture)
        {
            // grab (fixture)item.id, and fixture details of said fixture
            SetProperties(fixture);
            
            OpenEditCommand = new Command(async () => await OpenEditPage());
        }

        #region Properties
        private string _fixtureId;
        public string FixtureId
        {
            get { return _fixtureId; }
            set { _fixtureId = value; }
        }

        private string _league;
        public string League
        {
            get { return _league; }
            set { _league = value; }
        }

        private string _batsTeam;
        public string BatsTeam
        {
            get { return _batsTeam; }
            set { _batsTeam = value; }
        }

        private string _teamVs;
        public string TeamVs
        {
            get { return _teamVs; }
            set { _teamVs = value; }
        }

        private string _venue;
        public string Venue
        {
            get { return _venue; }
            set { _venue = value; }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }
        #endregion

        void SetProperties(object fixture)
        {
            IsBusy = true;

            var prop = fixture.GetType().GetProperties();

            FixtureId = (string)prop[0].GetValue(fixture);
            League = (string)prop[1].GetValue(fixture);
            BatsTeam = (string)prop[2].GetValue(fixture);
            TeamVs = (string)prop[3].GetValue(fixture);
            Venue = (string)prop[4].GetValue(fixture);
            Date = (string)prop[9].GetValue(fixture);
            Time = (string)prop[6].GetValue(fixture);

            IsBusy = false;
        }
        public async Task OpenEditPage()
        {
            // TODO Open up edit page for fixtures
        }
    }
}
