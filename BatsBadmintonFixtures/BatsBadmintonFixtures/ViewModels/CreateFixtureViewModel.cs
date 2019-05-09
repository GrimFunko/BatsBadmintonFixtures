using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            Title = "Create Fixture";
            PageOpenEvent += CreateFixtureViewModel_PageOpenEvent;
            CreateFixtureCommand = new Command(async () => await CreateFixture());
            CloseWindowCommand = new Command(() => Utilities.Navigation.PopModalAsync());

            NewFixture = new Fixture();

            Teams = new ObservableRangeCollection<Team>();
            if (Cache.Contains("Teams"))
                PopulateTeams((string)Cache.Get("Teams"));
            else
                PageOpenEvent?.Invoke(this, EventArgs.Empty);

            _teamVs = new ValidatableObject<string>();
            _fixtureVenue = new ValidatableObject<string>();
            _minimumDate = DateTime.Now.Date;
            
        }


        #region Properties
        public ICommand CreateFixtureCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

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

        private DateTime _fixtureDate;
        public DateTime FixtureDate
        {
            get { return _fixtureDate; }
            set
            {
                SetProperty(ref _fixtureDate, value);
                NewFixture.Date = value;
            }
        }

        private DateTime _minimumDate;

        public DateTime MinimumDate
        {
            get { return _minimumDate; }
            set { _minimumDate = value; }
        }


        private ValidatableObject<string> _teamVs;
        public ValidatableObject<string> TeamVs
        {
            get { return _teamVs; }
            set
            {
                SetProperty(ref _teamVs, value);
            }
        }

        private TimeSpan _fixtureTime;
        public TimeSpan FixtureTime
        {
            get { return _fixtureTime; }
            set
            {
                SetProperty(ref _fixtureTime, value);
                NewFixture.Time = value;
            }
        }

        private ValidatableObject<string> _fixtureVenue;
        public ValidatableObject<string> FixtureVenue
        {
            get { return _fixtureVenue; }
            set
            {
                SetProperty(ref _fixtureVenue, value);
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

        private async Task CreateFixtureMock()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            ResetForm();
            await Application.Current.MainPage.DisplayAlert("Success!", "Created fixture.", "OK.");

            IsBusy = false;
        }

        private async Task CreateFixture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            if (!ValidEntries())
            {
                IsBusy = false;
                return;
            }
            NewFixture.Venue = _fixtureVenue.Value;
            NewFixture.TeamVs = _teamVs.Value;

            string post = Utilities.GetJsonString(NewFixture);
            var strcon = new StringContent(post, Encoding.UTF8, "application/json");

            try
            {
                using (var response = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/fixtures/add", strcon))
                {
                    string str = await response.Content.ReadAsStringAsync();
                    var srm = ServerResponseMessage.FromJson(str);

                    if (!response.IsSuccessStatusCode)
                        await Application.Current.MainPage.DisplayAlert($"Error! {(int)response.StatusCode} {response.ReasonPhrase}.", srm.Message, "OK");
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", srm.Message, "OK.");
                        ResetForm();
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok.");
            }
            finally
            {
                IsBusy = false;
            }

            // Double check NewFixture properties are filled, then post to API
            
        }

        // Clears form entries, leaving only the selected team for ease of use
        private void ResetForm()
        {
            NewFixture = new Fixture();
            _teamVs.Value = "";
            FixtureDate = MinimumDate;
            FixtureTime = new TimeSpan(0, 0, 0);
            _fixtureVenue.Value = "";
        }
        
        private bool ValidEntries()
        {
            return _fixtureVenue.IsValid && _teamVs.IsValid && (NewFixture.Date != null) && (NewFixture.Time != null) && (NewFixture.BatsTeam != null);
        }

        private void PopulateTeams(string teamsJson)
        {
            var teamArray = Team.FromJson(teamsJson);
            Teams.ReplaceRange(teamArray);
        }


    }
}
