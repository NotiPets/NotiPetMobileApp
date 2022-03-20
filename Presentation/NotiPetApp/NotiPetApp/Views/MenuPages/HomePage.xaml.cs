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
    public partial class HomePage 
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = App.Current.Container.Resolve(typeof(HomeViewModel));

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel!=null)
            {
                this.OneWayBind(ViewModel, vm => vm.AppMenuItems, view => view.MenuLayout.ItemSource)
                    .DisposeWith(ViewDisposables);
            }

        }
    }
}