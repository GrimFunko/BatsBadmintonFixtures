﻿using MvvmHelpers;
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
	public partial class RegistrationSuccessPage : ContentPage
	{
		public RegistrationSuccessPage (BaseViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}