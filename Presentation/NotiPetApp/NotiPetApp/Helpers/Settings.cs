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
    }
}