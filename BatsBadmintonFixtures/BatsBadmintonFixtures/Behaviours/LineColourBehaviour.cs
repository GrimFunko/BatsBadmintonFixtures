using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BatsBadmintonFixtures
{
    public static class LineColourBehaviour
    {
        public static readonly BindableProperty ApplyLineColourProperty =
            BindableProperty.CreateAttached("ApplyLineColour", typeof(bool), typeof(LineColourBehaviour), false, propertyChanged: OnApplyLineColourChanged);

        public static readonly BindableProperty LineColourProperty =
            BindableProperty.CreateAttached("LineColour", typeof(Color), typeof(LineColourBehaviour), Color.Default);

        public static bool GetApplyLineColour(BindableObject view)
        {
            return (bool)view.GetValue(ApplyLineColourProperty);
        }

        public static void SetApplyLineColour(BindableObject view, bool value)
        {
            view.SetValue(ApplyLineColourProperty, value);
        }

        public static Color GetLineColour (BindableObject view)
        {
            return (Color)view.GetValue(LineColourProperty);
        }

        public static void SetLineColour(BindableObject view, Color value)
        {
            view.SetValue(LineColourProperty, value);
        }

        private static void OnApplyLineColourChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            bool hasLine = (bool)newValue;
            if (hasLine)
            {
                view.Effects.Add(new EntryLineColourEffect());
            }
            else
            {
                var entryLineColourEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColourEffect);
                if (entryLineColourEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColourEffectToRemove);   
                }
            }
        }
    }
}
