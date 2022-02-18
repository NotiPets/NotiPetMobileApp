using NotiPet.Data.Services;
using NotiPet.Mocks.Services;
using Prism.Ioc;
using Prism.Modularity;

namespace NotiPet.Mocks
{
    public class MocksModule:IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAssetServiceApi, AssetServiceApiMock>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}