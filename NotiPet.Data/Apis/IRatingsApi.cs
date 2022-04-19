using System;
using System.Net.Http;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using Refit;

namespace NotiPet.Data
{
    public interface IRatingsApi
    {
        [Get("/Ratings/ByBusiness/{businessId}")]
        IObservable<HttpResponseMessage> GetRatingsByBusiness(int businessId);
        [Post("/Ratings")]
        IObservable<HttpResponseMessage> PostRatings([Body] ReviewDto reviewDto);
    }
}