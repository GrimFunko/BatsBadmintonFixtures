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
            SelectedFixture = fixture as Fixture;
            Title = Date.ToString("dd-MM-yyyy");
            
            //OpenEditCommand = new Command(async () => await OpenEditPage());
        }

        #region Properties

        public bool PlayerAuthorised
        {
            get
            {
                if (AppCurrent.User is Player)
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

        private Fixture _selectedFixture;
        public Fixture SelectedFixture { get { return _selectedFixture; } set { SetProperty(ref _selectedFixture, value); } }

        public bool FullTeam { get {return SelectedFixture.BatsTeam.FullTeam; } }

        public string FixtureId
        {
            get { return SelectedFixture.FixtureId; }
        }

        public string League { get { return SelectedFixture.BatsTeam.League; } }

        public string BatsTeam
        {
            get { return SelectedFixture.BatsTeam.TeamName; }
        }



        public string TeamVs
        {
            get { return SelectedFixture.TeamVs; }
        }

        public string Venue
        {
            get { return SelectedFixture.Venue; }
        }

        public DateTime Date
        {
            get { return SelectedFixture.Date; }
        }

        public TimeSpan Time
        {
            get { return SelectedFixture.Time; }
        }
        #endregion



      
    }
}
// TODO Open up edit page for fixture changes
// TODO Get fixture players on page open
// TODO Add, edit, remove fixture players
// TODO Follow players to profile page


