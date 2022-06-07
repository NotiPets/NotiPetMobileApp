using ReactiveUI;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage 
    {
        public EditProfilePage()
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
    }
}