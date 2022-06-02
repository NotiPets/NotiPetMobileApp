using System;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Dtos
{
    public class ReviewDto
    {
        public string Id { get; set; }


        public int BusinessId { get; set; }

        public BusinessDto Business { get; set; }


        public string UserId { get; set; }
        public UserDto User { get; set; }



        public string Comment { get; set; }

        public int RatingNumber { get; set; }
        public DateTime Created { get; set; } 
    }
}