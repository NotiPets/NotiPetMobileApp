using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage 
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Username,vw=>vw.UserEntry.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Email,vw=>vw.EmailEntry.ValidationMessage));
            disposables.Add(this.WhenAnyValue(x=>x.ViewModel.ErrorMessage)
                              .StartWith(string.Empty)
                            .Select(e=> !string.IsNullOrEmpty(e))
                            .BindTo(this, x=> x.AuthenticationErrorMessageLabel.IsVisible));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Password,vw=>vw.PasswordEntry.ValidationMessage));
            return base.ManageDisposables(disposables); 
        }
    }
}