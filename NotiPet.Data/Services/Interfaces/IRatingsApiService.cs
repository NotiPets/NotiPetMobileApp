using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public interface IRatingsApiService
    {
        IObservable<IEnumerable<ReviewDto>> GetReviewsByBusinessId(int businessId);
        IObservable<ReviewDto> CreateReviews(ReviewDto map);
    }
}