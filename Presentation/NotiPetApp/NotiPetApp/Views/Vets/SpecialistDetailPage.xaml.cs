using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Vets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecialistDetailPage 
    {
        public SpecialistDetailPage()
        {
            WhenAppearing.InvokeCommand(ViewModel,x=>x.InitializeCommand);
            InitializeComponent();
           
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
           
        }
    }
}