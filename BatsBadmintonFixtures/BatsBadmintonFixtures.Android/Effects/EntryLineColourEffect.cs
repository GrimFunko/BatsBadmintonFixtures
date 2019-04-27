using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using BatsBadmintonFixtures.Droid.Effects;

[assembly:ResolutionGroupName ("BBF")]
[assembly:ExportEffect (typeof(EntryLineColourEffect),"EntryLineColourEffect")]

namespace BatsBadmintonFixtures.Droid.Effects
{
    public class EntryLineColourEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as EditText;
                UpdateLineColour();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColourBehaviour.LineColourProperty.PropertyName)
            {
                UpdateLineColour();
            }
        }

        private void UpdateLineColour()
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(LineColourBehaviour.GetLineColour(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}