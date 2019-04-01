using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Config;
using MvvmHelpers;

namespace BatsBadmintonFixtures
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FixturesPage : ContentPage
	{
        ListView listView;
		public FixturesPage (BaseViewModel viewModel, AccessLevels access)
		{
            BindingContext = viewModel;
			InitializeComponent ();
            listView = _listView;

            if (access >= AccessLevels.captain)
            {
                ToolbarItem addFixture = new ToolbarItem() { Text = "Add" };
                addFixture.Clicked += AddFixture_Clicked;
                ToolbarItems.Add(addFixture);               
            }
		}

        private void AddFixture_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(Factory.CreatePage(typeof(CreateFixturePage)));
        }

        public void _ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //TODO finish implementing mvvm
            var item = listView.SelectedItem;
            if (item != null)
            {
                Navigation.PushAsync(Factory.CreatePage(typeof(FixtureDetailPage), typeof(FixtureDetailViewModel), item, true));
                listView.SelectedItem = null;
            }
        }

        private void _listView_Refreshing(object sender, EventArgs e)
        {
            listView.IsRefreshing = false;
        }
    }
}