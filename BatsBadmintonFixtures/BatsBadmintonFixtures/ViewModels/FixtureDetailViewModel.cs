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
        public ICommand OpenEditCommand { get; set; }

        public FixtureDetailViewModel(object fixture)
        {
            // grab (fixture)item.id, and fixture details of said fixture
            SetProperties(fixture);
            _fixture = fixture as Fixture;
            Title = Date;
            
            //OpenEditCommand = new Command(async () => await OpenEditPage());
        }

        #region Properties
        public bool CaptainAuthorised
        {
            get
            {
                if (CurrentUser.AccessLevel >= AccessLevels.captain)
                    return true;
                return false;
            }
        }

        public bool PlayerAuthorised
        {
            get
            {
                if (CurrentUser.AccessLevel >= AccessLevels.player)
                    return true;
                return false;
            }
        }

        public bool IsHomeMatch
        {
            get {
                if (Venue == "Home")
                    return true;
                else
                    return false;
            }
        }
        public bool IsAwayMatch { get { return !IsHomeMatch; } }

        private Fixture _fixture { get; set; }

        public bool FullTeam { get; set; }

        private string _fixtureId;
        public string FixtureId
        {
            get { return _fixtureId; }
            set { _fixtureId = value; }
        }

        private string _league;
        public string League
        {
            get { return _fixture.League; }
            //set { _league = value; }
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

            var fix = fixture as Fixture;

            FixtureId = fix.ID;
            //League = fix.League;
            BatsTeam = fix.BatsTeam;
            TeamVs = fix.TeamVs;
            Venue = fix.Venue;
            Date = fix.FixtureListDateFormat;
            Time = fix.Time;
            FullTeam = fix.FullTeam;

            IsBusy = false;
        }

      
    }
}
// TODO Open up edit page for fixture changes
// TODO Get fixture players on page open
// TODO Add, edit, remove fixture players
// TODO Follow players to profile page


