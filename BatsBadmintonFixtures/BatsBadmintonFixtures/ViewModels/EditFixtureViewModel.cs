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
        public ICommand CloseWindowCommand { get; set; }
        
        public static event EventHandler<EventArgs> FixtureChangedEvent;


        public EditFixtureViewModel(object fixture)
        {
            UpdateFixtureCommand = new Command(async () => await UpdateFixture());
            DeleteFixtureCommand = new Command(async () => await DeleteFixture());
            CloseWindowCommand = new Command(() => Utilities.Navigation.PopModalAsync());

            _selectedFixture = fixture as Fixture;

            _teamVs = new ValidatableObject<string>();
            Venues = new ObservableRangeCollection<string> { "Home", "Away" };

            SetProperties();
            
        }

        #region Properties
        private Fixture _selectedFixture;
        public Fixture SelectedFixture
        {
            get { return _selectedFixture; }
            set { _selectedFixture = value; }
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
                SelectedFixture.Date = value;
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
                SelectedFixture.TeamVs = value.Value;
            }
        }

        private TimeSpan _fixtureTime;
        public TimeSpan FixtureTime
        {
            get { return _fixtureTime; }
            set
            {
                SetProperty(ref _fixtureTime, value);
                SelectedFixture.Time = value;
            }
        }

        private string _fixtureVenue;
        public string FixtureVenue
        {
            get { return _fixtureVenue; }
            set
            {
                SetProperty(ref _fixtureVenue, value);
                SelectedFixture.Venue = value;
            }
        }

        public ObservableRangeCollection<string> Venues { get; set; }


        #endregion

        private void SetProperties()
        {
            SelectedTeam = _selectedFixture.BatsTeam;
            _minimumDate = DateTime.Now.Date;
            FixtureDate = _selectedFixture.Date;
            FixtureTime = _selectedFixture.Time;
            FixtureVenue = _selectedFixture.Venue;
            _teamVs.Value = _selectedFixture.TeamVs;
        }

        private async Task UpdateFixture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            SelectedFixture.Venue = FixtureVenue;
            SelectedFixture.TeamVs = TeamVs.Value;

            string post = Utilities.GetJsonString(SelectedFixture);
            var strcon = new StringContent(post, Encoding.UTF8, "application/json");

            bool success = false;

            try
            {
                using (var response = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/fixtures/update", strcon))
                {
                    string str = await response.Content.ReadAsStringAsync();
                    var srm = ServerResponseMessage.FromJson(str);

                    if (!response.IsSuccessStatusCode)
                        await Application.Current.MainPage.DisplayAlert($"Error! {(int)response.StatusCode} {response.ReasonPhrase}.", srm.Message, "OK");
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", srm.Message, "OK.");
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok.");
            }
            finally
            {
                IsBusy = false;
                if (success)
                {
                    FixtureChangedEvent?.Invoke(this, EventArgs.Empty);
                    Utilities.Navigation.NavigationStack[1].BindingContext = Factory.CreateViewModel(typeof(FixtureDetailViewModel), _selectedFixture);
                    await Utilities.Navigation.PopModalAsync();
                }
            }
        }

        private async Task UpdateFixtureMock()
        {
            SelectedFixture.Venue = FixtureVenue;
            SelectedFixture.TeamVs = TeamVs.Value;
            Utilities.Navigation.NavigationStack[1].BindingContext = Factory.CreateViewModel(typeof(FixtureDetailViewModel), _selectedFixture);
            FixtureChangedEvent?.Invoke(this, EventArgs.Empty);

            await Application.Current.MainPage.DisplayAlert("Updated Successfully!", "The fixture changes have been recorded.", "Ok.");
            // save to cache
        }

        private async Task DeleteFixture()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            var action = await Application.Current.MainPage.DisplayAlert("Confirmation", "Are you sure you want to delete this fixture?", "Yes", "No");

            if (!action)
            {
                IsBusy = false;
                return;
            }
               

            string post = Utilities.GetJsonString(SelectedFixture);
            var strcon = new StringContent(post, Encoding.UTF8, "application/json");
            try
            {
                using (var resp = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/fixtures/delete", strcon))
                {
                    var str = await resp.Content.ReadAsStringAsync();
                    var srm = ServerResponseMessage.FromJson(str);

                    if (resp.IsSuccessStatusCode)
                    {
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Success!", srm.Message, "Ok.");
                        FixtureChangedEvent?.Invoke(this, EventArgs.Empty);
                        Utilities.ReturnToRoot();
                    }
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

        public async Task DeleteFixtureMock()
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "Fixture deleted!", "Ok.");
            FixtureChangedEvent?.Invoke(this, EventArgs.Empty);
            Utilities.ReturnToRoot();
        }

    }
}
