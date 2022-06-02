using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class RatingsApiService:BaseApiService,IRatingsApiService
    {
        private readonly IApiClient<IRatingsApi> _apiClient;

        public RatingsApiService(IApiClient<IRatingsApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<ReviewDto>> GetReviewsByBusinessId(int businessId)
        {

            return RemoteRequestObservableAsync<IEnumerable<ReviewDto>>(_apiClient.Client.GetRatingsByBusiness(businessId))
                .Select(e=>e.Result);
        }

        public IObservable<ReviewDto> CreateReviews(ReviewDto map)
        {
            return RemoteRequestObservableAsync<ReviewDto>(_apiClient.Client.PostRatings(map))
                .Select(e=>e.Result);
        }
    }
}