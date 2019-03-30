using BatsBadmintonFixtures.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BatsBadmintonFixtures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            Detail = new NavigationPage(Factory.CreatePage(typeof(FixturesPage), true));
            Detail.Title = "Upcoming Fixtures";
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            
            if (Detail.Title != page.Title)
            {
                Detail = new NavigationPage(page);
                Detail.Title = page.Title;
            }

            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}