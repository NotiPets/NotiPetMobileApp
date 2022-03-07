using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
        public static IObservable<string> Token
        {
            get => Observable.FromAsync(token => GetSecureStorage(nameof(Token))); 
            set=>value.Select(e=>SetSecureStorage(nameof(Username),e));
        }
        static Task<string> GetSecureStorage(string key)
           => DeviceInfo.Platform != DevicePlatform.Unknown ? SecureStorage.GetAsync(key):Task.FromResult(string.Empty);
       
      static  Task SetSecureStorage(string key,string value)
            => DeviceInfo.Platform != DevicePlatform.Unknown ? SecureStorage.SetAsync(key,value):Task.CompletedTask;
    }
}