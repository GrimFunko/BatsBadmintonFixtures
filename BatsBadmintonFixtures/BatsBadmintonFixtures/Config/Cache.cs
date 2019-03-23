using BatsBadmintonFixtures.Models;
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

        public static User GetUser(string itemName = "CurrentUser")
        {
            AccessLevels access = (AccessLevels)Get("CurrentUserAccessLevel");
            switch (access)
            {
                case AccessLevels.unauthorised:
                    return null;
                case AccessLevels.guest:
                    return (User)Get(itemName);
                case AccessLevels.player:
                    return (Player)Get(itemName);
                case AccessLevels.captain:
                    return (Captain)Get(itemName);
                case AccessLevels.admin:
                    return (Admin)Get(itemName);
                default:
                    return null;
            }
        }

    }
}
