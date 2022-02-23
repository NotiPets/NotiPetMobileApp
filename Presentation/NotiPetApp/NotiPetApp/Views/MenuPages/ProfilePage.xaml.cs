using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage 
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.OneWayBind(ViewModel,x=>x.LogOutCommand,vw=>vw.CellLogout.Command  ));
            return base.ManageDisposables(disposables);
        }
    }
}