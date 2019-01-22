using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BatsBadmintonFixtures.ViewModels;

namespace BatsBadmintonFixtures
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FixturesPage : ContentPage
	{
        ListView listView;
		public FixturesPage ()
		{
			InitializeComponent ();
            listView = _listView;
		}
        
        public void _ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = listView.SelectedItem;
            if (item != null)
            {
                Navigation.PushAsync(new FixtureDetailPage(item));
                listView.SelectedItem = null;
            }
        }
        
	}
}