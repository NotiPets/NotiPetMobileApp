using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage 
    {
        public RegisterPage()
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
            disposables.Add(this.WhenAnyValue(e => e.ViewModel.PersonalDocument)
                .Select(e => e != null)
                .BindTo(this, e => e.DocumentIdLB.IsVisible));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Name,vw=>vw.UsernameLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.LastName,vw=>vw.LastnameLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Username,vw=>vw.UsernameLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Password,vw=>vw.PasswordLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Email,vw=>vw.EmailLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.DocumentType,vw=>vw.DocumentIdLB.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Phone,vw=>vw.PhoneLb.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.City,vw=>vw.CityLb.ValidationMessage));
            disposables.Add(this.BindValidation(ViewModel,wm=>wm.Province,vw=>vw.ProvinceLB.ValidationMessage));
            return base.ManageDisposables(disposables); 
        }
    }
}