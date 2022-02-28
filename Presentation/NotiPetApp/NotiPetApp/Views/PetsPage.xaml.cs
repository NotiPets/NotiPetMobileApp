using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetsPage 
    {
        public PetsPage()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.OneWayBind(ViewModel,vm=>vm.Pets,vw=>vw.Pets.ItemsSource));
            return base.ManageDisposables(disposables);
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel != null)
            {
                WhenAppearing
                    .InvokeCommand(ViewModel?.InitializingCommand);
            }
        }
    }
}