using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using System.Net;
using System.Net.Http;
using System.ComponentModel;

using MvvmHelpers;
using Xamarin.Forms;
using Newtonsoft.Json;

using BatsBadmintonFixtures.Config;
using BatsBadmintonFixtures.Models;
using System.IO;
using BatsBadmintonFixtures.ValidationRules;

namespace BatsBadmintonFixtures.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        // Page Control Commands
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        // Validation Commands
        public ICommand ValidateUsernameCommand { get; set; }
        public ICommand ValidatePasswordCommand { get; set; }
        public ICommand ValidateConfirmPasswordCommand { get; set; }
        public ICommand ValidateEmailCommand { get; set; }
        public ICommand ValidateFirstNameCommand { get; set; }
        public ICommand ValidateSurnameCommand { get; set; }
        public ICommand ValidateTel1Command { get; set; }
        public ICommand ValidateTel2Command { get; set; }
        public ICommand ValidateFBLinkCommand { get; set; }

        public RegistrationViewModel()
        {
            Title = "Bats registration";      
            
            SubmitCommand = new Command(async () => await SubmitRegistrationDetails());
            CancelCommand = new Command(() => Application.Current.MainPage = Factory.CreatePage(typeof(LoginPage), typeof(LoginViewModel)));

            ValidateUsernameCommand = new Command(() => ValidateUsername());
            ValidatePasswordCommand = new Command(() => ValidatePassword());
            ValidateConfirmPasswordCommand = new Command(() => ValidateConfirmPassword());
            ValidateEmailCommand = new Command(() => ValidateEmail());
            ValidateFirstNameCommand = new Command(() => ValidateFirstName());
            ValidateSurnameCommand = new Command(() => ValidateSurname());
            ValidateTel1Command = new Command(() => ValidateTel1());
            ValidateTel2Command = new Command(() => ValidateTel2());
            ValidateFBLinkCommand = new Command(() => ValidateFBLink());

            // Initialising ValidatableObject s
            _username = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _firstName = new ValidatableObject<string>();
            _surname = new ValidatableObject<string>();
            _telephone1 = new ValidatableObject<string>();
            _telephone2 = new ValidatableObject<string>();
            _fbLink = new ValidatableObject<string>();

            AddValidationRules();
        }

        // Properties
        #region Props
        private ValidatableObject<string> _username;
        public ValidatableObject<string> Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get { return _password; }
            set { SetProperty(ref _password,value); }
        }

        private ValidatableObject<string> _confirmPassword;
        public ValidatableObject<string> ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private ValidatableObject<string> _firstName;
        public ValidatableObject<string> FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private ValidatableObject<string> _surname;
        public ValidatableObject<string> Surname
        {
            get { return _surname; }
            set { SetProperty(ref _surname, value); }
        }

        private ValidatableObject<string> _telephone1;
        public ValidatableObject<string> Telephone1
        {
            get { return _telephone1; }
            set { SetProperty(ref _telephone1, value); }
        }

        private ValidatableObject<string> _telephone2;
        public ValidatableObject<string> Telephone2
        {
            get { return _telephone2; }
            set { SetProperty(ref _telephone2, value); }
        }

        private ValidatableObject<string> _fbLink;
        public ValidatableObject<string> FBLink
        {
            get { return _fbLink; }
            set { SetProperty(ref _fbLink, value); }
        }

        #endregion

        // Validation Methods
        #region Validations
        
        /// <summary>
        /// Add the validation rules to validatable objects
        /// </summary>
        private void AddValidationRules()
        {
            // Username Validation Rules
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username is required!" });
            _username.Validations.Add(new LengthRule<string> { ValidationMessage = "Username must be between 6-24 characters.", MinimumLength = 6, MaximumLength = 24 });
            _username.Validations.Add(new ContainsNoSpecialCharactersRule<string> { ValidationMessage = "Username should not contain special characters." });
            _username.Validations.Add(new NoSpacesRule<string> { ValidationMessage = "Username must not contain spaces." });

            // Password Validation Rules
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is required!" });
            _password.Validations.Add(new LengthRule<string> { ValidationMessage = "Password must be between 6-54 characters.", MinimumLength = 6, MaximumLength = 54 });
            _password.Validations.Add(new NoSpacesRule<string> { ValidationMessage = "Password must not contain spaces." });

            // Confirm Password Rules
            _confirmPassword.Validations.Add(new MatchValuesRule<string> { ValidationMessage = "Passwords do not match!" });

            // Email Validation Rules
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is required!" });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Must be a valid email." });

            // ContactTel Validation Rules
            //tel1
            _telephone1.Validations.Add(new PhoneNumberRule<string> { ValidationMessage = "Not a valid phone number." });
            //tel2
            _telephone2.Validations.Add(new PhoneNumberRule<string> { ValidationMessage = "Not a valid phone number." });
            

        }

        private bool ValidateEntries()
        {
            return ValidateUsername() && ValidatePassword() && ValidateConfirmPassword() && ValidateEmail() &&
                ValidateFirstName() && ValidateSurname() && ValidateTel1() && ValidateTel2() && ValidateFBLink();
        }

        private bool ValidateUsername()
        {
            return _username.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool ValidateConfirmPassword()
        {
            if (_confirmPassword.Validations.Count == 0)
                return true;

            // Updates MatchValuesRule.OtherValue to reflect current value of Password
            var vr = _confirmPassword.Validations[0] as MatchValuesRule<string>;
            vr.OtherValue = Password.Value;
            _confirmPassword.Validations[0] = vr;

            return _confirmPassword.Validate();
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidateFirstName()
        {
            return _firstName.Validate();
        }

        private bool ValidateSurname()
        {
            return _surname.Validate();
        }

        private bool ValidateTel1()
        {
            return _telephone1.Validate();
        }

        private bool ValidateTel2()
        {
            return _telephone2.Validate();
        }

        private bool ValidateFBLink()
        {
            return _fbLink.Validate();
        }

        #endregion

        private async Task SubmitRegistrationDetails()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            bool success = false;

            if (!ValidateEntries())
            {
                IsBusy = false;
                return;
            }
                

            try
            {
                User newUser = new User(Username.Value,Email.Value,Password.Value,ConfirmPassword.Value,
                    FirstName.Value,Surname.Value,Telephone1.Value,Telephone2.Value,FBLink.Value);

                var post = Utilities.GetJsonString(newUser);

                var strcon = new StringContent(post, Encoding.UTF8, "application/json");
               
                using (var response = await Utilities.ApiClient.PostAsync(Utilities.ApiClient.BaseAddress + "/user/register",
                                                                        strcon))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Open new successful submition page
                        string json = await response.Content.ReadAsStringAsync();
                        var resp = ServerResponseMessage.FromJson(json);

                        await Application.Current.MainPage.DisplayAlert("Submitted", resp.Message, "OK");
                        success = true;
                    }
                    else // Generalised server response model to show "message"
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var resp = ServerResponseMessage.FromJson(json);
                        await Application.Current.MainPage.DisplayAlert($"Error! {(int)response.StatusCode} {response.ReasonPhrase}.", resp.Message.TrimStart(' '), "OK");
                    }
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
                Application.Current.MainPage = Factory.CreatePage(typeof(LoginPage), typeof(LoginViewModel));
        }


    }
}
