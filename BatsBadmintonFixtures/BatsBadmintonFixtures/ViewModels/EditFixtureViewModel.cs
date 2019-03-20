using System;
using System.Collections.Generic;
using System.Text;

using MvvmHelpers;
using BatsBadmintonFixtures.Models;
using BatsBadmintonFixtures.Config;

namespace BatsBadmintonFixtures.ViewModels
{
    public class EditFixtureViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Team> Teams { get; set; }
        public EditFixtureViewModel()
        {
            Teams = new ObservableRangeCollection<Team>();
            if (Cache.Contains("Team"))
                AddTeamsToCollection(Team.FromJson((string)Cache.Get("Team")));
        }

        public void AddTeamsToCollection(Team[] teams)
        {
            Teams.ReplaceRange(teams);
        }

        public void GetTeams()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                using (var response = Utilities.ApiClient.GetAsync(Utilities.ApiClient.BaseAddress + "/teams/"))
                {
                    response.RunSynchronously();

                }
                
            }
            catch(Exception ex)
            {

            }
        }
    }
}
