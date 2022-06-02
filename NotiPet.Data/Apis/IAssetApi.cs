using System;
using System.Net.Http;
using Refit;

namespace NotiPet.Data
{
    public interface IAssetApi
    {
        [Get("/AssetsServices")]
        public IObservable<HttpResponseMessage> GetAssetService();
        [Get("/AssetsServices/ByBusiness/{id}")]
        IObservable<HttpResponseMessage> GetAssetServiceByBusinessId(int id);
    }
}