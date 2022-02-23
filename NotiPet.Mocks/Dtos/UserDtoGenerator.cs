using System;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Services
{
    public class UserDtoGenerator
    {
        public IEnumerable<UserRoleDto> Veterinaries { get; set; }
        public UserRoleDto UserRoleDto { get; set; }

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



        }
    }
}