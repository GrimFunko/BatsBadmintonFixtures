using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BatsBadmintonFixtures.ViewModels
{
    class CreateFixtureViewModel : BaseViewModel
    {
        public CreateFixtureViewModel()
        {
            PageOpenEvent += CreateFixtureViewModel_PageOpenEvent;
            CreateFixtureCommand = new Command(async () => await CreateFixture());

            NewFixture = new Fixture();

            Teams = new ObservableRangeCollection<Team>();
            if (Cache.Contains("Teams"))
                PopulateTeams((string)Cache.Get("Teams"));
            else
                PageOpenEvent?.Invoke(this, EventArgs.Empty);

            
        }


        #region Properties
        public ICommand CreateFixtureCommand { get; set; }

        public ObservableRangeCollection<Team> Teams { get; set; }

        public Fixture NewFixture;

        public event EventHandler<EventArgs> PageOpenEvent;

        private Team _batsTeam;
        public Team BatsTeam
        {
            get { return _batsTeam; }
            set
            {
                _batsTeam = value;
                NewFixture.BatsTeam = value;
            }
        }

        private string _fixtureDate;
        public string FixtureDate
        {
            get { return _fixtureDate; }
            set
            {
                _fixtureDate = value;
                NewFixture.Date = value;
            }
        }

        private string _teamVs;
        public string TeamVs
        {
            get { return _teamVs; }
            set
            {
                _teamVs = value;
                NewFixture.TeamVs = value;
            }
        }

        private string _fixtureTime;
        public string FixtureTime
        {
            get { return _fixtureTime; }
            set
            {
                _fixtureTime = value;
                NewFixture.Time = value;
            }
        }

        private string _fixtureVenue;
        public string FixtureVenue
        {
            get { return _fixtureVenue; }
            set
            {
                _fixtureVenue = value;
                NewFixture.Venue = value;
            }
        }
#endregion

        private async void CreateFixtureViewModel_PageOpenEvent(object sender, EventArgs e)
        {
            await GetTeams();
        }

        public async Task GetTeams()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                using (var response = await Utilities.ApiClient.GetAsync(Utilities.ApiClient.BaseAddress + "/teams/"))
                {
                    var teams = await response.Content.ReadAsStringAsync();
                    Cache.Save("Teams", teams);
                    PopulateTeams(teams);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CreateFixture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            // Double check NewFixture properties are filled, then post to API
            
        }

        private void PopulateTeams(string teamsJson)
        {
            var teamArray = Team.FromJson(teamsJson);
            Teams.ReplaceRange(teamArray);
        }


    }
}
