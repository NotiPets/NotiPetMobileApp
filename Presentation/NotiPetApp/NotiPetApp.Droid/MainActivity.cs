using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.GoogleClient;
using Prism;
using Prism.Ioc;
using FFImageLoading.Forms.Platform;

namespace NotiPetApp.Droid
{
    [Activity(Label = "NotiPetApp", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            
            GoogleClientManager.Initialize(this);
            LoadApplication(new App(new AndroidInitializer()));
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
        }
    }

    public class AndroidInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
          
        }
    }
}