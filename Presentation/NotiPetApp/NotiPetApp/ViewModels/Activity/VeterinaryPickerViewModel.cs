using System.Reactive;
using System.Reactive.Linq;
using DynamicData.Binding;
using MvvmHelpers.Commands;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels.Activity
{
    public class VeterinaryPickerViewModel:VeterinaryViewModel,IInitialize,IConfirmNavigation
    {
        private CreateAppointment _createAppointment;
        
        public VeterinaryPickerViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IVeterinaryService veterinaryService, ISchedulerProvider schedulerProvider) : base(navigationService, pageDialogService, veterinaryService, schedulerProvider)
        {

            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }
        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode()==NavigationMode.New&&parameters.ContainsKey(ParameterConstant.VeterinaryPickerAppointment))
            {
                _createAppointment = parameters[ParameterConstant.VeterinaryPickerAppointment] as CreateAppointment;
            }
    
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode()==NavigationMode.New)
            {
                _createAppointment.BusinessId = (int)parameters[ParameterConstant.VeterinaryId] ;
                parameters.Add(ParameterConstant.VeterinaryPickerAppointment,_createAppointment);
                
            }

            return true;
        }
    }
}