using MvvmHelpers;
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
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage (BaseViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
            formPage1.IsVisible = true;
            formPage2.IsVisible = false;
            tel2.IsVisible = false;

            nextButton.Clicked += NextFormPage;
            previousButton.Clicked += PreviousFormPage;
            extraNumber.Clicked += ShowTel2;

            usernameField.TextChanged += FieldTextSizeChange;
            passwordField.TextChanged += FieldTextSizeChange;
            confPasswordField.TextChanged += FieldTextSizeChange;
            emailField.TextChanged += FieldTextSizeChange;
            firstNameField.TextChanged += FieldTextSizeChange;
            surnameField.TextChanged += FieldTextSizeChange;
            contactTelField.TextChanged += FieldTextSizeChange;
            tel2.TextChanged += FieldTextSizeChange;
            facebookField.TextChanged += FieldTextSizeChange;
        }

        void NextFormPage(object sender, EventArgs e)
        {
            formPage1.IsVisible = false;
            formPage2.IsVisible = true;
            Subtitle.Text = "Registration 2 of 2";
        }

        void PreviousFormPage(object sender, EventArgs e)
        {
            formPage1.IsVisible = true;
            formPage2.IsVisible = false;
            Subtitle.Text = "Registration 1 of 2";
        }

        void ShowTel2 (object sender, EventArgs e)
        {
            tel2.IsVisible = true;
            extraNumber.IsVisible = false;
        }

        void FieldTextSizeChange(object sender, EventArgs e)
        {
            var obj = sender as Entry;
            if (obj.Text != "")
                obj.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry));
            else
                obj.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
        }
    }
}