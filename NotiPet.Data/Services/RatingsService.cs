using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class RatingsService:IRatingsService
    {
        private readonly IRatingsApiService _ratingsApiService;
        private readonly IMapper _mapper;
        private SourceCache<Review, string> _reviewsSource = new SourceCache<Review, string>(x => x.Id);
        public RatingsService(IRatingsApiService ratingsApiService,IMapper mapper)
        {
            _ratingsApiService = ratingsApiService;
            _mapper = mapper;
        }

        public SourceCache<Review, string> ReviewsSource => _reviewsSource;

        public IObservable<IEnumerable<Review>> GetReviewsByBusinessId(int businessId)
        {
            return _ratingsApiService.GetReviewsByBusinessId(businessId)
                 .Select(_mapper.Map<IEnumerable<Review>>)
                .Do(ReviewsSource.AddOrUpdate);
        }

        public IObservable<Review> CreateReviews(Review review)
        {
          return  _ratingsApiService.CreateReviews(_mapper.Map<ReviewDto>(review)).Select(_mapper.Map<Review>);
        }
    }
}