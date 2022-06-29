using System;
using AutoMapper;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Mappers
{
    public  class DateTimeOffsetToDateTimeConverter : ITypeConverter<DateTimeOffset, DateTime>
    {
        public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
        {
            return source.DateTime;
        }
    }
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetConfig()
        {
            var automapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTimeOffset, DateTime>()
                    .ConvertUsing<DateTimeOffsetToDateTimeConverter>();
                cfg.CreateMap<AssetServiceTypeDto, AssetServiceType>();
                cfg.CreateMap<UserDto, User>()
                    .ForMember(e=>e.FullName,x=>x.Ignore())
                    .ForMember(e=>e.FullAddress,x=>x.Ignore())

                    .ReverseMap();
                cfg.CreateMap<AssetServiceDto, AssetServiceModel>()
                    .ForMember(e => e.PictureUrl, x => x.Ignore())
                    .ReverseMap();
                cfg.CreateMap<PetTypeDto, PetType>().ReverseMap();
                cfg.CreateMap<PetDto, Pet>()
                    .ReverseMap();
                
                cfg.CreateMap<BusinessDto, Veterinary>()
                    .ReverseMap();
              
                cfg.CreateMap<AuthenticationDto,Authentication>();

                cfg.CreateMap<IAuthenticationRequestViewModel,RequestAuthenticationDto>(); 
                cfg.CreateMap<SpecialistDto,Specialist>()
                    .ReverseMap();
                cfg.CreateMap<SaleDto, Sales>()
                    .ForMember(x=>x.Veterinary,x=>x.MapFrom(x=>x.Business));
                cfg.CreateMap<RequestOrder, RequestOrderDto>();
                cfg.CreateMap<Order, OrderDto>()
                    .ReverseMap();
                cfg.CreateMap<SpecialityDto, Speciality>()
                    .ReverseMap();
                cfg.CreateMap<AppointmentDto, Appointment>()
                    .ReverseMap();
                cfg.CreateMap<ReviewDto,Review >()
                    .ReverseMap();
                cfg.CreateMap<TicketDto,Ticket >()
                    .ReverseMap();
                cfg.ShouldUseConstructor = x =>! x.IsPrivate;
            }); 
            automapper.AssertConfigurationIsValid();
            return automapper;
        }
    }
}