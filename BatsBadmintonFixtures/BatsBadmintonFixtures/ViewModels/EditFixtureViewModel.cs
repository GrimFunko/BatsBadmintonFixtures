using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using MvvmHelpers;
using BatsBadmintonFixtures.Models;
using BatsBadmintonFixtures.Config;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;

namespace BatsBadmintonFixtures.ViewModels
{
    public class EditFixtureViewModel : BaseViewModel
    {
        public ICommand UpdateFixtureCommand { get; set; }
        public ICommand DeleteFixtureCommand { get; set; }
        
        public event EventHandler<EventArgs> PageOpenEvent;

        public EditFixtureViewModel(object fixture = null)
        {
            PageOpenEvent += EditFixtureViewModel_PageOpenEvent;
            DeleteFixtureCommand = new Command(async () => await DeleteFixture());

            _fixture = fixture as Fixture;

            if (!Cache.Contains("Teams"))
                PageOpenEvent?.Invoke(this, EventArgs.Empty);

            _fixtureVenue = new ValidatableObject<string>();
            _teamVs = new ValidatableObject<string>();

            SetProperties();
        }

        #region Properties
        private Fixture _fixture;
        public Fixture Fixture
        {
            get { return _fixture; }
            set { SetProperty(ref _fixture, value); }
        }

        private Team _selectedTeam;
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }

        private DateTime _fixtureDate;
        public DateTime FixtureDate
        {
            get { return _fixtureDate; }
            set
            {
                SetProperty(ref _fixtureDate, value);
                Fixture.Date = value.ToString("yyyy-MM-dd");
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
                Fixture.TeamVs = value.Value;
            }
        }

        private TimeSpan _fixtureTime;
        public TimeSpan FixtureTime
        {
            get { return _fixtureTime; }
            set
            {
                SetProperty(ref _fixtureTime, value);
                Fixture.Time = value.ToString(@"hh\:mm");
            }
        }

        private ValidatableObject<string> _fixtureVenue;
        public ValidatableObject<string> FixtureVenue
        {
            get { return _fixtureVenue; }
            set
            {
                SetProperty(ref _fixtureVenue, value);
                Fixture.Venue = value.Value;
            }
        }

        #endregion

        private async void EditFixtureViewModel_PageOpenEvent(object sender, EventArgs e)
        {
            await GetTeams();
        }

        private void SetProperties()
        {
            SelectedTeam = _fixture.BatsTeam;
            _minimumDate = DateTime.Now.Date;
            var comp = _fixture.Date.Split('-');
            FixtureDate = new DateTime(Convert.ToInt16(comp[0]), Convert.ToInt16(comp[1]),Convert.ToInt16(comp[2]));
            FixtureTime = TimeSpan.Parse(_fixture.Time);
            _fixtureVenue.Value = _fixture.Venue;
            _teamVs.Value = _fixture.TeamVs;
        }

        //TODO Need to flesh out the implementation of DeleteFixture, such as returning to fixtures page after deleting, making sure Cache is in sync etc. 
        private async Task DeleteFixture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            string post = Utilities.GetJsonString(Fixture);
            var strcon = new StringContent(post, Encoding.UTF8, "application/json");
            try
            {
                using (var resp = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/fixtures/delete", strcon))
                {
                    var str = await resp.Content.ReadAsStringAsync();
                    var srm = ServerResponseMessage.FromJson(str);

                    if (resp.IsSuccessStatusCode)
                        await Application.Current.MainPage.DisplayAlert("Success!", srm.Message, "Ok.");
                    else
                        await Application.Current.MainPage.DisplayAlert($"Error! {(int)resp.StatusCode} {resp.ReasonPhrase}", srm.Message, "Ok.");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetTeams()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                using (var response = await Utilities.ApiClient.GetAsync(Utilities.ApiClient.BaseAddress + "/teams/"))
                {
                    var teams = await response.Content.ReadAsStringAsync();
                    Cache.Save("Teams", teams);
                }               
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
