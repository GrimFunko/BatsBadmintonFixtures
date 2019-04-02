using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json;
using BatsBadmintonFixtures.Models;
using Xamarin.Forms;
using MvvmHelpers;

namespace BatsBadmintonFixtures.Config
{
    public static class Utilities
    {
        public static HttpClient ApiClient { get; set; }

        public static string GetJsonString(object obj) => JsonConvert.SerializeObject(obj);

        public static void InitialiseClient()
        {
            ConfigurationData cd = new ConfigurationData();

            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };
            ApiClient = new HttpClient(clientHandler);
            ApiClient.BaseAddress = cd.BaseAddress;
            
        }
    }

    /// <summary>
    /// Enum depicting the roles within the application.
    /// </summary>
    public enum AccessLevels
    {
        unauthorised = -1,
        guest = 0,
        player = 1,
        captain = 2,
        admin = 3
    };

    public static class AppCurrent
    {
        public static User User { get; set; }
        
    }

    public static class Factory
    {
        public static User CreateUser(AccessLevels access)
        {
            switch (access)
            {
                case AccessLevels.unauthorised:
                    return null;
                case AccessLevels.guest:
                    return new User();
                case AccessLevels.player:
                    return new Player();
                case AccessLevels.captain:
                    return new Captain();
                case AccessLevels.admin:
                    return new Admin();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates a Page and passes in a ViewModel through its constructor. Also allows for extra information to be bound to.
        /// </summary>
        /// <param name="viewType">This is the class type of the View.</param>
        /// <param name="viewModel">This is the class type of the corresponding ViewModel.</param>
        /// <param name="item">This allows extra data to be passed into the ViewModel e.g. when selecting a list object.</param>
        /// <param name="accessDependant">Represents whether a Page has user access level dependant features.</param>
        /// <returns></returns>
        public static Page CreatePage(Type viewType, Type viewModel = null, object item = null, bool accessDependant = false)
        {
            // ViewModel == null should only occur in the case of HomePage
            if (viewModel == null)
                return (Page)Activator.CreateInstance(viewType);

            // Creates a ViewModel based on the type passed in, and whether there is extra data
            var vm = (item == null) ? CreateViewModel(viewModel) : CreateViewModel(viewModel, item);

            // Creates the page with corresponding view model and adds the current user access level if needed
            return accessDependant ? (Page)Activator.CreateInstance(viewType, vm,(AccessLevels)Enum.Parse(typeof(AccessLevels), (string)Cache.Get("CurrentUserAccessLevel"))) :
                    (Page)Activator.CreateInstance(viewType, vm);
            
        }

        /// <summary>
        /// Creates a ViewModel and allows for a data object to be passed into its constructor.
        /// </summary>
        /// <param name="viewModel">This is the class type of the target ViewModel.</param>
        /// <param name="item">This is an optional object for passing data into the ViewModels constructor.</param>
        /// <returns></returns>
        public static BaseViewModel CreateViewModel(Type viewModel, object item = null)
        {
            return (item == null) ? (BaseViewModel)Activator.CreateInstance(viewModel) : (BaseViewModel)Activator.CreateInstance(viewModel, item);
        }



    }
}
