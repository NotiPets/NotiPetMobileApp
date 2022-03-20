using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Vets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecialistView 
    {
        public SpecialistView()
        {
            InitializeComponent();
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel !=null)
            {
                this.WhenPropertyChanged(x => x.ViewModel)
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel?.InitializeCommand)
                    .DisposeWith(ViewDisposables);
            }

        }
    }
}