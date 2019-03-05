using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BatsBadmintonFixtures.Config
{
    public static class Cache
    {
        public static bool Contains(string itemName)
        {
            return Application.Current.Properties.ContainsKey(itemName);
        }

        public static void Save(string itemName, object item)
        {
            Application.Current.Properties[itemName] = item;
        }

        public static object Get(string itemName)
        {
            return Application.Current.Properties[itemName];
        }

        // Removes all cached items, except for remembered login details
        public static void RemoveAll()
        {
            var login = Contains("LoginDetails") ? Get("LoginDetails") : null;
            Application.Current.Properties.Clear();
            if (login != null)
                Save("LoginDetails", login);
        }

        public static void Remove(string itemName)
        {
            Application.Current.Properties[itemName] = null;
        }

    }
}
