﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BatsBadmintonFixtures
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FixturesPage : ContentPage
	{
		public FixturesPage ()
		{
			InitializeComponent ();
            
		}
        
        private void ViewSelectedFixture(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new FixtureDetailPage());
                  
        } 
        
	}
}