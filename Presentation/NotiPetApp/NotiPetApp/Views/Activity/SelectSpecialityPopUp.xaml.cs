using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectSpecialityPopUp 
    {
        public SelectSpecialityPopUp()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            this.WhenAnyValue(x => x.ViewModel)
                .Select(e=>Unit.Default)
                .InvokeCommand(ViewModel?.InitializeCommand);
            return base.ManageDisposables(disposables);
        }
    }
}