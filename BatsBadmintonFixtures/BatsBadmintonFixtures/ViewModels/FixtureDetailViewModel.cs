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
            _fixture = fixture as Fixture;
            Title = Date;
            
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

        public Fixture _fixture { get; set; }

        public bool FullTeam { get {return _fixture.BatsTeam.FullTeam; } }

        public string FixtureId
        {
            get { return _fixture.FixtureId; }
        }

        public string League { get { return _fixture.BatsTeam.League; } }

        public string BatsTeam
        {
            get { return _fixture.BatsTeam.TeamName; }
        }



        public string TeamVs
        {
            get { return _fixture.TeamVs; }
        }

        public string Venue
        {
            get { return _fixture.Venue; }
        }

        public string Date
        {
            get { return _fixture.Date; }
        }

        public string Time
        {
            get { return _fixture.Time; }
        }
        #endregion



      
    }
}
// TODO Open up edit page for fixture changes
// TODO Get fixture players on page open
// TODO Add, edit, remove fixture players
// TODO Follow players to profile page


