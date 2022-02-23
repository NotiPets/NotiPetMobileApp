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
    public partial class OptionsParametersPage 
    {
        public OptionsParametersPage()
        {
            InitializeComponent();
            
            
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.OneWayBind(ViewModel,vm=>vm.ParameterOptions, vw=>vw.Parameters.ItemsSource));
            return base.ManageDisposables(disposables);
        }
    }
}