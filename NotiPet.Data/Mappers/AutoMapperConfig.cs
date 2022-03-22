using AutoMapper;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Mappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetConfig()
        {
            var automapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AssetServiceTypeDto, AssetServiceType>();
                cfg.CreateMap<UserDto, User>()
                    .ForMember(e=>e.FullName,x=>x.Ignore())
                    .ForMember(e=>e.FullAddress,x=>x.Ignore())

                    .ReverseMap();
                cfg.CreateMap<AssetServiceDto, AssetServiceModel>()
                    .ForMember(e => e.Guid, x => x.Ignore());
                cfg.CreateMap<PetTypeDto, PetType>().ReverseMap();
                cfg.CreateMap<PetDto, Pet>()
                    .ForMember(e=>e.Guid,x=>x.Ignore()) 
                    .ReverseMap();
                
                cfg.CreateMap<BusinessDto, Veterinary>();
                cfg.CreateMap<AuthenticationDto,Authentication>(); 
                cfg.CreateMap<IAuthenticationRequestViewModel,RequestAuthenticationDto>(); 
                cfg.CreateMap<SpecialistDto,Specialist>()
                    .ReverseMap(); 
                cfg.CreateMap<SpecialityDto,Speciality>()
                    .ReverseMap(); 
 
                cfg.ShouldUseConstructor = x =>! x.IsPrivate;
            }); 
            automapper.AssertConfigurationIsValid();
            return automapper;
        }
    }
}