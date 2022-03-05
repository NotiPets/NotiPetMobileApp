using System;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Services
{
    public class UserDtoGenerator
    {
        public IEnumerable<UserRoleDto> Veterinaries { get;  }
        public UserRoleDto UserRoleDto { get; }
        public AuthenticationDto AuthenticationDto { get;  }

        public UserDtoGenerator()
        {
            var user = new Faker<UserDto>().RuleFor(e => e.Name, x => x.Person.FirstName)
                .RuleFor(e => e.Lastname, x => x.Person.LastName)
                .RuleFor(e => e.Address1, x => x.Person.Address.Street)
                .RuleFor(e => e.City, x => x.Person.Address.City)
                .RuleFor(e => e.Province, x => x.Person.Address.Suite)
                .RuleFor(e => e.Phone, x => x.Person.Phone);
            var userRole = new Faker<UserRoleDto>()
                .RuleFor(e => e.Username, x => x.Person.FullName)
                .RuleFor(e => e.UserId, x => 1)
                .RuleFor(e => e.Password, x => "0000")
                .RuleFor(e => e.RoleId, x => x.Random.Int(0, 2))
                .RuleFor(e => e.Update, x => x.Date.Recent())
                .RuleFor(e => e.User, x => user);

            UserRoleDto = userRole.Generate();
            Veterinaries = userRole.Generate(100);
            AuthenticationDto = new AuthenticationDto()
            {
                Email = "fiji1984@eiibps.com",
                Password = "12345",
                Token ="424e953b-3fa2-4ecb-a9ac-e1076b5c920c",
                IsRegister = true
            };



        }
    }
}