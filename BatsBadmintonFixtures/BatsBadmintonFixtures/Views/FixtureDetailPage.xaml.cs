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
        private object _selectedItem;
        public object SelectedItem { get { return _selectedItem; } set {_selectedItem = value; } }

        // TODO Build and implement the rest of the fixture details page
        public FixtureDetailPage (object item)
		{
            SelectedItem = item;
            BindingContext = SelectedItem;
            InitializeComponent ();
            
		}
	}
}