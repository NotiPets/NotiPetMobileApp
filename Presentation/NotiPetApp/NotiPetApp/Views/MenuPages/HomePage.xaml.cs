using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
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
            this.OneWayBind(ViewModel, vm => vm.AppMenuItems, view => view.MenuLayout.ItemSource)
                .DisposeWith(PageDisposables);
        }
    }
}