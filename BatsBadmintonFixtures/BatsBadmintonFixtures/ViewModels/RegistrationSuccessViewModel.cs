using BatsBadmintonFixtures.Config;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BatsBadmintonFixtures.ViewModels
{
    public class RegistrationSuccessViewModel : BaseViewModel
    {
        public ICommand ReturnToLoginPageCommand { get; set; }

        private string _successMessage;
        public string SuccessMessage
        {
            get { return _successMessage; }
            set { _successMessage = value; }
        }

        public RegistrationSuccessViewModel(object successMessage)
        {
            _successMessage = successMessage as string;
            ReturnToLoginPageCommand = new Command(() => ReturnToLoginPage());
        }

        public void ReturnToLoginPage()
        {
            Application.Current.MainPage = Factory.CreatePage(typeof(LoginPage), typeof(LoginViewModel));
        }
    }
}
