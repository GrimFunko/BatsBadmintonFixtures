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
	public partial class CreateFixturePage : ContentPage
	{
		public CreateFixturePage (BaseViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}