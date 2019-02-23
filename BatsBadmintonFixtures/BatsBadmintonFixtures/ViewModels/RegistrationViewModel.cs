using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using System.ComponentModel;
using MvvmHelpers;
using Xamarin.Forms;
using Newtonsoft.Json;

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;
using System.Threading.Tasks;
using System.Net.Http;

namespace BatsBadmintonFixtures.ViewModels
{
    class RegistrationViewModel : BaseViewModel, IDataErrorInfo
    {
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public RegistrationViewModel()
        {
            Title = "Bats registration";
            //Page1 = true;
            //Page2 = false;
            // send registration details command
            SubmitCommand = new Command(async () => await SubmitRegistrationDetails());
            CancelCommand = new Command(() => Application.Current.MainPage = new LoginPage());
        }


        #region FormProperties
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        private string _telephone1;
        public string Telephone1
        {
            get { return _telephone1; }
            set { _telephone1 = value; }
        }

        private string _telephone2;
        public string Telephone2
        {
            get { return _telephone2; }
            set { _telephone2 = value; }
        }

        private string _fbLink;
        public string FBLink
        {
            get { return _fbLink; }
            set { _fbLink = value; }
        }

        //private bool _page1;

        //public bool Page1
        //{
        //    get { return _page1; }
        //    set { _page1 = value; }
        //}

        //private bool _page2;

        //public bool Page2
        //{
        //    get { return _page2; }
        //    set { _page2 = value; }
        //}


        #endregion

        private async Task SubmitRegistrationDetails()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            bool success = false;

            try
            {
                object details = new
                {
                    type = "user-registration",
                    username = Username,
                    password = Password,
                    cpassword = ConfirmPassword,
                    email = Email,
                    first_name = FirstName,
                    surname = Surname,
                    tel1 = Telephone1,
                    tel2 = Telephone2,
                    fbLink = FBLink
                };

                var post = Utilities.GetJsonString(details);

                using (var response = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/user/register",
                                                                        new StringContent(post, Encoding.UTF8, "application/json")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var resp = RegistrationResponse.FromJson(json);

                        await Application.Current.MainPage.DisplayAlert("Successfully submitted", resp.Message, "OK");
                        success = true;
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Something went wrong.", response.ReasonPhrase, "OK");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Something went wrong",
                                    ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            if (success)
                Application.Current.MainPage = new LoginPage();
        }


        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

    }
}
