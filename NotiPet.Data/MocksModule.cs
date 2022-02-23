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

            containerRegistry.RegisterSingleton<IStoreService, StoreService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
           // containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IMapper>(x=> new Mapper(AutoMapperConfig.GetConfig()));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}