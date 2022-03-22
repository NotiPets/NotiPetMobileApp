using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotiPetApp.Helpers
{
    public static class Settings
    {
        public static string Username
        {
            get=>DeviceInfo.Platform != DevicePlatform.Unknown? Preferences.Get(nameof(Username),string.Empty):string.Empty; 
            set=>Preferences.Set(nameof(Username),value);
        }
        public static IObservable<string> Token => Observable.FromAsync(token => GetSecureStorage(nameof(Token)));

        public static  Task SetToken(string token)
        {
           return SetSecureStorage(nameof(Token),token);
        }
        static Task<string> GetSecureStorage(string key)
           => DeviceInfo.Platform != DevicePlatform.Unknown ? SecureStorage.GetAsync(key):Task.FromResult(string.Empty);
       
      static  Task SetSecureStorage(string key,string value)
            => DeviceInfo.Platform != DevicePlatform.Unknown ? SecureStorage.SetAsync(key,value):Task.CompletedTask;
    }
}