using System;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Mocks.Services
{
    public class UserDtoGenerator
    {
        public UserDto UserRoleDto { get; set; }
        public AuthenticationDto AuthenticationDto { get;  }

        public UserDtoGenerator()
        {
            var user = new Faker<UserDto>().RuleFor(e => e.Names, x => x.Person.FirstName)
                .RuleFor(e => e.Id, x => $"{x.IndexFaker}")
                .RuleFor(e => e.Lastnames, x => x.Person.LastName)
                .RuleFor(e => e.Username, x => x.Person.UserName)
                .RuleFor(e => e.Password, x => Guid.NewGuid().ToString())
                .RuleFor(e => e.Email, x => x.Person.Email)
                .RuleFor(e => e.Address1, x => x.Person.Address.Street)
                .RuleFor(e => e.Address2, x => x.Person.Address.Street)
                .RuleFor(e => e.PictureUrl, x => x.Image.LoremFlickrUrl(keywords: "person"))
                .RuleFor(e => e.City, x => x.Person.Address.City)
                .RuleFor(e => e.Province, x => x.Person.Address.Suite)
                .RuleFor(e => e.Document, x => Guid.NewGuid().ToString())
                .RuleFor(e => e.Phone, x => x.Person.Phone);
            




            UserRoleDto = user.Generate();
            AuthenticationDto = new AuthenticationDto()
            {

                Jwt = "424e953b-3fa2-4ecb-a9ac-e1076b5c920c",
                User = UserRoleDto,
            };



        }

        public static UserDto  GenerateUser()
        {
            return new Faker<UserDto>().RuleFor(e => e.Names, x => x.Person.FirstName)
                .RuleFor(e => e.Id, x => $"{x.IndexFaker}")
                .RuleFor(e => e.Lastnames, x => x.Person.LastName)
                .RuleFor(e => e.Username, x => x.Person.UserName)
                .RuleFor(e => e.Password, x => Guid.NewGuid().ToString())
                .RuleFor(e => e.Email, x => x.Person.Email)
                .RuleFor(e => e.Address1, x => x.Person.Address.Street)
                .RuleFor(e => e.Address2, x => x.Person.Address.Street)
                .RuleFor(e => e.PictureUrl, x => x.Image.LoremFlickrUrl(keywords: "person"))
                .RuleFor(e => e.City, x => x.Person.Address.City)
                .RuleFor(e => e.Province, x => x.Person.Address.Suite)
                .RuleFor(e => e.Document, x => Guid.NewGuid().ToString())
                .RuleFor(e => e.Phone, x => x.Person.Phone);
        }
       
    }
}