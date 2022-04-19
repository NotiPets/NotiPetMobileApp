using AutoMapper;
using NotiPet.Data.Mappers;
using NotiPet.Data.Repositories;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using Prism.Ioc;
using Prism.Modularity;
using Realms;

namespace NotiPet.Data
{
    public class DataModule:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApiProvider>(provider => new ApiProvider("https://notipet-api.herokuapp.com/api"));
            
            containerRegistry.RegisterSingleton<IAssetServiceApi, AssetServiceApi>();
            containerRegistry.RegisterSingleton<IUserServiceApi,UserServiceApi>(); 
            containerRegistry.RegisterSingleton<IPetServiceApi,PetServiceApi>();
            containerRegistry.RegisterSingleton<IBusinessServiceApi,BusinessServiceApi>();
            containerRegistry.RegisterSingleton<ISpecialistServiceApi,SpecialistsServiceApi>();
            containerRegistry.RegisterSingleton<IRatingsService,RatingsService>();
            containerRegistry.RegisterSingleton<IRatingsApiService,RatingsApiService>();
            containerRegistry.RegisterSingleton<ISalesServiceApi,SalesServiceApi>();
            containerRegistry.RegisterSingleton(typeof(IApiClient<>), typeof(ApiClient<>));
            containerRegistry.RegisterSingleton<IStoreService, StoreService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<IPetsService, PetsService>();
            containerRegistry.RegisterSingleton<IMapper>(x=> new Mapper(AutoMapperConfig.GetConfig()));
            containerRegistry.RegisterSingleton<IUserService,UserService>();  
            containerRegistry.RegisterSingleton<IVeterinaryService, VeterinaryService>();
            containerRegistry.RegisterSingleton<ISpecialistsService,SpecialistsService>();
            containerRegistry.RegisterSingleton<ISalesService,SalesService>();
            containerRegistry.RegisterSingleton<IDataBaseProvider<Realm>, RealmDatabaseProvider>(); 
            containerRegistry.RegisterSingleton(typeof(IDataSource<>), typeof(Repository<>));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}