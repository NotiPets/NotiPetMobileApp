using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Pets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VaccinesPage 
    {
        public VaccinesPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel != null)
            {
                WhenAppearing
                    .InvokeCommand(ViewModel?.InitializeCommand);
            }
        }
    }
}