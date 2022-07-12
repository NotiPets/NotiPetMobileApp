using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;
using NotiPetApp.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage 
    {
        public StorePage()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.OneWayBind(ViewModel,vm=>vm.ParameterOptions,vw=>vw.Parameters.ItemSource));
            return base.ManageDisposables(disposables);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel != null)
            {
                WhenAppearing
                    .InvokeCommand(ViewModel?.InitializeDataCommand);
            }
        }

    }
}