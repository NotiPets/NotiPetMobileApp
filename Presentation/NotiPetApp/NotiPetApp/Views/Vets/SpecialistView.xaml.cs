using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;
using NotiPetApp.Helpers;
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
            this.WhenPropertyChanged(x => x.ViewModel)
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel?.InitializeCommand)
                    .DisposeWith(ViewDisposables);
            
        }
    }
}