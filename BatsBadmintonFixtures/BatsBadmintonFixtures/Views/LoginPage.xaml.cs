﻿using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BatsBadmintonFixtures
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(BaseViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
