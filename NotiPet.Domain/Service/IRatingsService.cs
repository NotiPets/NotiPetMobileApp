using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IRatingsService
    {
        public SourceCache<Review,string> ReviewsSource { get;  }
        public IObservable<IEnumerable<Review>> GetReviewsByBusinessId(int businessId);
        public IObservable<Review> CreateReviews(Review review);
    }
}