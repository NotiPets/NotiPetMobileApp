using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Dtos
{
    public class SpecialityDtoGenerator
    {
        public static  List<SpecialityDto>  SpecialistDtos { get; set; }

        public SpecialityDtoGenerator()
        {
            SpecialistDtos = new List<SpecialityDto>()
            {
                new SpecialityDto()
                {
                    Name = "Veterniario"
                },
                new SpecialityDto()
                {
                    Name = "Terapista"
                },
                new SpecialityDto()
                {
                    Name = "Anestesiólogo"
                },
                new SpecialityDto()
                {
                    Name = "Gastroenterólogo"
                },
            };
        }
        public static SpecialityDto GenerateDto()
        {
            return SpecialistDtos?.First();
        }
    }
}