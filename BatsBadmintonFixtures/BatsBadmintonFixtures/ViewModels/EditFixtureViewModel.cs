using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using MvvmHelpers;
using BatsBadmintonFixtures.Models;
using BatsBadmintonFixtures.Config;
using System.Threading.Tasks;

namespace BatsBadmintonFixtures.ViewModels
{
    public class EditFixtureViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Team> Teams { get; set; }

        public event EventHandler<EventArgs> PageOpenEvent;

        public EditFixtureViewModel(object fixture)
        {
            Teams = new ObservableRangeCollection<Team>();
            PageOpenEvent += EditFixtureViewModel_PageOpenEvent;

            _fixture = fixture as Fixture;
            SelectedTeam = _fixture.BatsTeam.FullName;

            if (Cache.Contains("Team"))
                AddTeamsToCollection(Team.FromJson((string)Cache.Get("Team")));
            else
                PageOpenEvent?.Invoke(this, EventArgs.Empty);
        }

        private Fixture _fixture { get; set; }

        private string _selectedTeam;

        public string SelectedTeam
        {
            get { return _selectedTeam; }
            set { SetProperty(ref _selectedTeam, value); }
        }


        private async void EditFixtureViewModel_PageOpenEvent(object sender, EventArgs e)
        {
            await GetTeams();
        }

        public void AddTeamsToCollection(Team[] teams)
        {
            Teams.ReplaceRange(teams);
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
                    Cache.Save("Team", teams);
                    AddTeamsToCollection(Team.FromJson(teams));
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
