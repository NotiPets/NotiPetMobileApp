using System;
using AutoMapper;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Mocks;
using NotiPetApp.ViewModels;
using NotiPetApp.Views;
using NotiPetApp.Views.MenuPages;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
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
            InitializeComponent();
            NavigationService.NavigateAsync("TabMenuPage");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<MocksModule>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IStoreService, StoreService>();
            containerRegistry.RegisterSingleton<IMapper>(x=> new Mapper(AutoMapperConfig.GetConfig()));
            containerRegistry.RegisterForNavigation<TabMenuPage>();
            containerRegistry.RegisterForNavigation<HomePage,HomeViewModel>();
            containerRegistry.RegisterForNavigation<AppointmentPage>();
            containerRegistry.RegisterForNavigation<StorePage,StoreViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage>();
        }

    }   
}