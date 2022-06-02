using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Dtos
{
    public class SpecialityDtoGenerator
    {
        public   List<SpecialityDto>  SpecialistDtos { get; set; }

        public SpecialityDtoGenerator()
        {
            SpecialistDtos = new List<SpecialityDto>()
            {
                new SpecialityDto()
                {
                    Name = "Veterniario", Description = "Veterinario Dentista"
                },
                new SpecialityDto()
                {
                    Name = "Terapista", Description = "Veterinario Dentista"
                },
                new SpecialityDto()
                {
                    Name = "Anestesiólogo" , Description = "Veterinario Dentista"
                },
                new SpecialityDto()
                {
                    Name = "Gastroenterólogo" , Description = "Veterinario Dentista"
                },
            };
        }
        public static SpecialityDto GenerateDto()
        {
            return new SpecialityDtoGenerator().SpecialistDtos.FirstOrDefault();
        }
    }
}