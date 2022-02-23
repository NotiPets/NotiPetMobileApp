using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialNetworkAuthenticationPage 
    {
        public SocialNetworkAuthenticationPage()
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
            disposables.Add(this.OneWayBind(ViewModel,x=>x.SocialNetworks,vw => vw.SocialNetworks.ItemSource));
            return base.ManageDisposables(disposables);
        }
    }
}