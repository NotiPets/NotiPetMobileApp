using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmAppointmentPage 
    {
        public ConfirmAppointmentPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel!=null)
            {
                WhenAppearing
                    .InvokeCommand(ViewModel?.InitializeCommand);
            }
           
        }
        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.Bind(viewModel:ViewModel,x=>x.Veterinary,x=>x.VeterinaryContentView.BindingContext));
            return base.ManageDisposables(disposables);
        }
    }
}