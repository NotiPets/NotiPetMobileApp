using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiPetApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Vets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VeterinaryView 
    {
        public VeterinaryView(VeterinaryViewModel veterinaryViewModel)
        {
            InitializeComponent();
            ViewModel = veterinaryViewModel;
        }
    }
}