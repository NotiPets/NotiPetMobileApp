using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage  
    {
        public StartPage()
        {
            InitializeComponent();

        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {

            disposables.Add( Observable.FromEventPattern((handler)
                        => LottieView.OnFinishedAnimation += handler,
                    (handler) => LottieView.OnFinishedAnimation -= handler)
                .Subscribe((e) =>
                {
                    ViewModel.IsAnimating = false;
                }));
           
            return base.ManageDisposables(disposables);
        }

        private void LottieView_OnOnFinishedAnimation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}