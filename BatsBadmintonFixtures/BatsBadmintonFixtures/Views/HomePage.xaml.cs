using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.ViewModels;
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
        HomePageMaster HPM { get; set; }

        public HomePage()
        {
            InitializeComponent();
            HPM = Factory.CreatePage(typeof(HomePageMaster), typeof(HomePageMasterViewModel)) as HomePageMaster;
            Master = HPM;
            HPM.MasterListView.ItemSelected += ListView_ItemSelected;
            Detail = new NavigationPage(Factory.CreatePage(typeof(FixturesPage), typeof(FixturesViewModel), null, true));
            Detail.Title = "Upcoming Fixtures";
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMenuItem;
            if (item == null)
                return;

            //var page = (Page)Activator.CreateInstance(item.TargetType);
            
            
            if (Detail.Title != item.Title)
            {
                var page = Factory.CreatePage(item.TargetType, item.ViewModelType, null, true);
                Detail = new NavigationPage(page);
                Detail.Title = page.Title;
            }

            IsPresented = false;

            HPM.MasterListView.SelectedItem = null;
        }
    }
}