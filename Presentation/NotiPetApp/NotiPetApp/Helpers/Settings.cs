using System;
using System.Reactive.Linq;
using Bogus.DataSets;
using Xamarin.Essentials;

namespace NotiPetApp.Helpers
{
    public static class Settings
    {
        public static string Username
        {
            get=>Preferences.Get(nameof(Username),string.Empty); 
            set=>Preferences.Set(nameof(Username),value);
        }

        public static string Password
        {
            get;
            set;
        }

        public static IObservable<string> Token        
        {
            get=> Observable.FromAsync(token => SecureStorage.GetAsync(nameof(Token))) ; 
            set=>value.Select(e=>SecureStorage.SetAsync(nameof(Username),e));
        }
    }
}