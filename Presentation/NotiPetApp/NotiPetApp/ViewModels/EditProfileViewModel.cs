using System.Collections.Generic;
using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public List<string> Gender { get; set; }

        public EditProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            Gender = new List<string>(){
                "Macho" , "Hembra"
            };
        }
    }
}