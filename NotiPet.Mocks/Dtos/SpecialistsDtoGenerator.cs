using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;
using NotiPet.Mocks.Services;

namespace NotiPet.Mocks.Dtos
{
    public class SpecialistsDtoGenerator
    {
        public readonly IEnumerable<SpecialistDto> Specialists;

        public SpecialistsDtoGenerator()
        {
            var specialist = new Faker<SpecialistDto>()
                .RuleFor(x=>x.User,UserDtoGenerator.GenerateUser)
                .RuleFor(x=>x.Id,x=>x.IndexFaker)
                .RuleFor(x=>x.Speciality,SpecialityDtoGenerator.GenerateDto);
            
            Specialists = specialist.GenerateLazy(300);
        }

        public static SpecialistDto GetSpecialist()
        {
            return new Faker<SpecialistDto>()
                .RuleFor(x=>x.User,UserDtoGenerator.GenerateUser)
                .RuleFor(x=>x.Id,x=>x.IndexFaker)
                .RuleFor(x=>x.Speciality,SpecialityDtoGenerator.GenerateDto);
        }
        
    }
}