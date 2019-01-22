using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using System.Net;
using System.Net.Http;

using MvvmHelpers;
using Xamarin.Forms;

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;

namespace BatsBadmintonFixtures.ViewModels
{
    class FixtureDetailViewModel : BaseViewModel
    {
        public bool AdminPrivilige { get
            {
                if ((string)Application.Current.Properties["UserAccessLevel"] == "admin")
                    return true;
                return false;
            }
        }
        public ICommand OpenEditCommand { get; set; }

        public FixtureDetailViewModel()
        {
            OpenEditCommand = new Command(() => OpenEditPage());
        }


        public void OpenEditPage()
        {
            // TODO Open up edit page for fixture
        }
    }
}
