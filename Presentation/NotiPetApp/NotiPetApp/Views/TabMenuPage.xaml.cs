using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiPetApp.ViewModels;
using NotiPetApp.Views.MenuPages;
using Prism.Ioc;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabMenuPage 
    {
        private readonly IContainerProvider _containerProvider;

        public TabMenuPage(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
            InitializeComponent();

        }


    }
}