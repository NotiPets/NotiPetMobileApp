using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Properties;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class HomeViewModel:BaseViewModel,IInitializeAsync
    {
        private readonly IDeviceUtils _deviceUtils;
        public ObservableCollection<AppMenuItem> AppMenuItems { get; set; }
       [Reactive] public string Username { get; set; }
        public HomeViewModel(INavigationService navigationService, IPageDialogService dialogPage,IDeviceUtils deviceUtils) : base(navigationService, dialogPage)
        {
            _deviceUtils = deviceUtils;
            Username = Settings.Username;
             
            AppMenuItems = new ObservableCollection<AppMenuItem>()
            {
                new(AppResources.Veterinary,"Veterinary",1,ReactiveCommand.CreateFromTask(NavigateToLastVisitVeterinary),SizeItem.Small),
                new(AppResources.Appointments,"Calendar",2,ReactiveCommand.CreateFromTask(NavigateToAppointment),SizeItem.Small),
                new(AppResources.Store,"Shop",3,ReactiveCommand.CreateFromTask(ShowNavigateToStory),SizeItem.Small),
                new(AppResources.Emergency,"Emergency",4,ReactiveCommand.CreateFromTask(CallEmergency),SizeItem.Large),
            };
        }

        async Task NavigateToLastVisitVeterinary()
        {
          await  NavigationService.NavigateAsync(ConstantUri.Veterinary);
        }
        async Task NavigateToAppointment()
        {
            await  NavigationService.NavigateAsync(ConstantUri.Appointment);
        }
        async Task ShowNavigateToStory()
        {
            await  NavigationService.NavigateAsync(ConstantUri.Store);
        }
         Task CallEmergency()
        {
            //TODO Call Emergency
            try
            {
                Xamarin.Essentials.PhoneDialer.Open("914");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                
            }
      
            return Task.CompletedTask;

        }

         public async Task InitializeAsync(INavigationParameters parameters)
         {
             await _deviceUtils.GetCurrentLocation();
         }
    }
}