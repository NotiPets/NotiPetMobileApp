using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Bogus.DataSets;
using NotiPetApp.Controls;
using NotiPetApp.Models;
using NotiPetApp.Views.Vets;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Syncfusion.XForms.Buttons;
using Xamarin.CommunityToolkit.Effects;

namespace NotiPetApp.ViewModels
{
    public class PetInformation
    {
        public string Name { get; set; }
        public bool Status { get; set; }
    }
    public class RegisterOrEditPetViewModel : BaseViewModel
    {
        private int _selectedIndex;

        
        public List<string> Gender { get; set; }
        
        public List<PetInformation> InformationAditional { get; set; }


        

        public RegisterOrEditPetViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            InformationAditional= new List<PetInformation>();
            
            InformationAditional.Add(new PetInformation{Name = "Castrado", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Vacunado", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Amigable con perros", Status = true });
            InformationAditional.Add(new PetInformation{Name = "Amigable con gatos", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Amigable con niños", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Rastreador", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Raza pura", Status = true});
            InformationAditional.Add(new PetInformation{Name = "Castrado", Status = true});
            Gender = new List<string>(){
                "Macho" , "Hembra"
            };
        }
    }
}