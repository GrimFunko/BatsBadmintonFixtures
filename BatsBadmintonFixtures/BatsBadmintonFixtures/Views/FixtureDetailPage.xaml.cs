using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BatsBadmintonFixtures.ViewModels;
using BatsBadmintonFixtures.Models;

namespace BatsBadmintonFixtures
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FixtureDetailPage : ContentPage
	{
        //private object _selectedItem;
        //public object SelectedItem { get { return _selectedItem; } set {_selectedItem = value; } }

        private Fixture _fixture { get; set; }

        // TODO Build and implement the rest of the fixture details page
        public FixtureDetailPage (object item)
		{
            InitializeComponent ();
            BindingContext = new FixtureDetailViewModel(item);
            if (EditHack.IsVisible)
            {
                ToolbarItem edit = new ToolbarItem() { Text = "Edit" };
                ToolbarItems.Add(edit);
                edit.Clicked += Edit_Clicked;
            }   
                

            _fixture = item as Fixture;
            var FullTeam = _fixture.BatsTeam.FullTeam;

            if (FullTeam)
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
            Navigation.PushModalAsync(new EditFixturePage(_fixture));
        }
    }
}
