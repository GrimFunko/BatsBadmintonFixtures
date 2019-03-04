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

        public static void RemoveAll()
        {
            // TODO Implement remove cache feature, leaving remembered login details (?)
        }

    }
}
