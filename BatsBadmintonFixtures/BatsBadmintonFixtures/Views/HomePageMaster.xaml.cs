using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BatsBadmintonFixtures.ViewModels;
using MvvmHelpers;

namespace BatsBadmintonFixtures
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {

        public ListView MasterListView { get; set; }

        public HomePageMaster(BaseViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
            MasterListView = MenuItemsListView;
        }


    }
}