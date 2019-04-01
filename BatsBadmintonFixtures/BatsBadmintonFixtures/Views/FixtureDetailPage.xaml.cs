using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Models;
using BatsBadmintonFixtures.Config;
using MvvmHelpers;

namespace BatsBadmintonFixtures
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FixtureDetailPage : ContentPage
	{
        private FixtureDetailViewModel fdvm;

        public FixtureDetailPage (BaseViewModel viewModel, AccessLevels access)
		{
            InitializeComponent ();
            BindingContext = viewModel;

            if (access >= AccessLevels.captain)
            {
                ToolbarItem edit = new ToolbarItem() { Text = "Edit" };
                edit.Clicked += Edit_Clicked;
                ToolbarItems.Add(edit);
            }

            fdvm = BindingContext as FixtureDetailViewModel;

            if (fdvm.FullTeam)
            {
                CPairLabel.IsVisible = true;
                CPos1.IsVisible = true;
                CPos2.IsVisible = true;
                CMem1.IsVisible = true;
                CMem2.IsVisible = true;

                Grid.SetRow(ResPairLabel, 6);
                Grid.SetRow(ResPos1, 6);
                Grid.SetRow(ResPos2, 7);
                Grid.SetRow(ResMem1, 6);
                Grid.SetRow(ResMem2, 7);
            }
            else
            {
                CPairLabel.IsVisible = false;
                CPos1.IsVisible = false;
                CPos2.IsVisible = false;
                CMem1.IsVisible = false;
                CMem2.IsVisible = false;
            }
            
		}

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(Factory.CreatePage(typeof(EditFixturePage), typeof(EditFixtureViewModel), fdvm._fixture));
        }
    }
}
