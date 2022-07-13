using System;
using System.Net.Http;
using Refit;

namespace NotiPet.Data
{
    public interface IAssetApi
    {
        [Get("/AssetsServices/ByBusiness/{id}")]
        public IObservable<HttpResponseMessage> GetAssetService(int id);
        [Get("/AssetsServices/ByBusiness/{id}")]
        IObservable<HttpResponseMessage> GetAssetServiceByBusinessId(int id);
    }
}