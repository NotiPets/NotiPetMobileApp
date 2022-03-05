using AutoMapper;
using NotiPet.Data;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using NotiPet.Mocks;
using NotiPet.Mocks.Services;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using NotiPetApp.Views;
using NotiPetApp.Views.Authentication;
using NotiPetApp.Views.MenuPages;
using NotiPetApp.Views.Vets;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ReactiveUI;
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
            RxApp.DefaultExceptionHandler = new RxExceptionHandler();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg0NDIxQDMxMzkyZTM0MmUzMFNLQ3ZJWkRQZkwyc2pYTXkzZCtyTStaOG5DeHpBaWg5djNaQ0RmK2R1QzQ9");
            InitializeComponent();
            NavigationService.NavigateAsync(ConstantUri.Start);
           
        }

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
            containerRegistry.RegisterForNavigation<HomePage,HomeViewModel>();
            containerRegistry.RegisterForNavigation<AppointmentPage>();
            containerRegistry.RegisterForNavigation<StorePage,StoreViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage>(); 
            containerRegistry.RegisterForNavigation<CreatePetPage,CreatePetViewModel>();
            containerRegistry.RegisterForNavigation<SocialNetworkAuthenticationPage,SocialNetworkAuthenticationViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage,LoginViewModel>();
            containerRegistry.RegisterForNavigation<StartPage,StartViewModel>();
            containerRegistry.RegisterForNavigation<PetsPage,PetsViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage,ProfileViewModel>();
            
            containerRegistry.RegisterForNavigation<OptionsParametersPage,OptionsParametersViewModel>(ConstantUri.OptionParameters);
            containerRegistry.Register<VeterinaryViewModel>();
            containerRegistry.Register<VeterinaryView>();
            
        }

    }
}