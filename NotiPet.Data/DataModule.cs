using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
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
            var api = "https://notipet-api.herokuapp.com/api";
            containerRegistry.RegisterSingleton<IApiProvider>(provider => new ApiProvider(api));
            
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
            containerRegistry.RegisterScoped<HubConnection>(x =>
                new HubConnectionBuilder().WithUrl($"{api}/Appointments/Inform").Build());
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}