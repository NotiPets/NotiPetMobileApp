using AutoMapper;
using NotiPet.Data;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using NotiPet.Mocks;
using NotiPet.Mocks.Services;
using NotiPetApp.Helpers;
using NotiPetApp.Properties;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using NotiPetApp.ViewModels.Activity;
using NotiPetApp.ViewModels.PopUp;
using NotiPetApp.Views;
using NotiPetApp.Views.Activity;
using NotiPetApp.Views.Authentication;
using NotiPetApp.Views.MenuPages;
using NotiPetApp.Views.Pets;
using NotiPetApp.Views.PopUp;
using NotiPetApp.Views.UserSettings;
using NotiPetApp.Views.Vets;
using Plugin.GoogleClient;
using Plugin.SharedTransitions;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services;
using ReactiveUI;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf",Alias = "FontSolid")]
[assembly: ExportFont("Raleway-Bold.ttf",Alias = "Bold")]
[assembly: ExportFont("Raleway-SemiBold.ttf",Alias = "SemiBold")]
[assembly: ExportFont("Raleway-Regular.ttf",Alias = "Regular")] 
namespace NotiPetApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer) : base(platformInitializer){}

        protected override void OnInitialized()
        {
         
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg0NDIxQDMxMzkyZTM0MmUzMFNLQ3ZJWkRQZkwyc2pYTXkzZCtyTStaOG5DeHpBaWg5djNaQ0RmK2R1QzQ9");
            LocalizationResourceManager.Current.PropertyChanged += (_, _) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
            InitializeComponent();  
            var dialog = ContainerProvider.Resolve<IPageDialogService>();
            RxApp.DefaultExceptionHandler = new RxExceptionHandler(dialog);
            NavigationService.NavigateAsync(ConstantUri.Start);
            
            
        }

        public static IContainerProvider ContainerProvider;
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        { 
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<MocksModule>();
            moduleCatalog.AddModule<DataModule>();
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISchedulerProvider>(() => new SchedulerProvider(RxApp.MainThreadScheduler,RxApp.TaskpoolScheduler));
            containerRegistry.RegisterForNavigation<TabMenuPage,TabMenuViewModel>();
            containerRegistry.RegisterForNavigation<SharedTransitionNavigationPage>();
            containerRegistry.Register<HomeViewModel>();
            containerRegistry.Register<HomePage>();
            containerRegistry.RegisterForNavigation<AppointmentPage,AppointmentViewModel>();
            containerRegistry.RegisterForNavigation<StorePage,StoreViewModel>();
            containerRegistry.Register<ProfilePage>(); 
            containerRegistry.Register<ProfileViewModel>();
            containerRegistry.Register<SpecialistView>();
            containerRegistry.Register<VeterinaryView>();
            containerRegistry.RegisterSingleton<IDeviceUtils,DeviceUtils>();
            containerRegistry.RegisterForNavigation<SocialNetworkAuthenticationPage,SocialNetworkAuthenticationViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage,LoginViewModel>();
            containerRegistry.RegisterForNavigation<StartPage,StartViewModel>();
            containerRegistry.RegisterForNavigation<PetsPage,PetsViewModel>();
            containerRegistry.RegisterForNavigation<VetTabPage,VetTabViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage,AboutViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage,SettingsViewModel>();
            containerRegistry.RegisterForNavigation<OnBoardingPage,OnBoardingViewModel>();
            containerRegistry.RegisterForNavigation<RegisterOrEditPetPage, RegisterOrEditPetViewModel>();
            containerRegistry.RegisterForNavigation<EditProfilePage, EditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SelectSpecialityPopUp,SelectSpecialityViewModel >();
            containerRegistry.RegisterForNavigation<RegisterPage,RegisterViewModel >();
            containerRegistry.RegisterForNavigation<ConfirmAppointmentPage, ConfirmAppointmentViewModel>();
            
            containerRegistry.RegisterForNavigation<VeterinaryDetailPage,VeterinaryDetailViewModel>();

            containerRegistry.RegisterForNavigation<OptionsParametersPage,OptionsParametersViewModel>(ConstantUri.OptionParameters);
            containerRegistry.RegisterForNavigation<SpecialistDetailPage, SpecialistDetailViewModel>(); 
            containerRegistry.RegisterForNavigation<VeterinaryPickerPage, VeterinaryPickerViewModel>();  
            containerRegistry.RegisterForNavigation<AppointmentCompletePage, AppointmentCompleteViewModel>();  
            containerRegistry.RegisterForNavigation<CreateReviewsPage, CreateReviewsViewModel>();  
            containerRegistry.RegisterForNavigation<EditAppointmentPage, EditAppointmentViewModel>();  
            containerRegistry.RegisterForNavigation<HelpPage,HelpPageViewModel>();
            containerRegistry.RegisterForNavigation<PetDetailPage,PetDetailViewModel>();
            containerRegistry.RegisterForNavigation<VaccinesPage,VaccinesViewModel>();
            containerRegistry.RegisterInstance<IGoogleClientManager>(CrossGoogleClient.Current);
            ContainerProvider = Container;


        }
        
      

    }
}