using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using NotiPetApp.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage 
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = App.Current.Container.Resolve(typeof(ProfileViewModel));
            ViewModel?.InitializeCommand.Execute().Subscribe().Dispose();
        }
        
    }
}