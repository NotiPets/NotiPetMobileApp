using AutoMapper;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using Prism.Ioc;
using Prism.Modularity;

namespace NotiPet.Data
{
    public class DataModule:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApiProvider>(provider => new ApiProvider("https://noti-pet-test.herokuapp.com/api"));
            containerRegistry.RegisterSingleton(typeof(IApiClient<>), typeof(ApiClient<>));
            containerRegistry.RegisterSingleton<IStoreService, StoreService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<IPetsService, PetsService>();
            containerRegistry.RegisterSingleton<IMapper>(x=> new Mapper(AutoMapperConfig.GetConfig()));
            containerRegistry.RegisterSingleton<IUserServiceApi, UserServiceApi>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}