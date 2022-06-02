using System;

namespace NotiPet.Domain.Models
{
    public class Review
    {
        public Review(string id, int businessId, Veterinary business, string userId, User user, string comment, int ratingNumber)
        {
            Id = id;
            BusinessId = businessId;
            Business = business;
            UserId = userId;
            User = user;
            Comment = comment;
            RatingNumber = ratingNumber;
        }

        public string Id { get; set; }


        public int BusinessId { get; set; }

        public Veterinary Business { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

        public string Comment { get; set; }

        public int RatingNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string FullName => User?.FullName;

    }
}