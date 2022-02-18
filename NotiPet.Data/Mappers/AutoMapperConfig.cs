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
                cfg.CreateMap<AssetServiceDto, AssetServiceModel>()
                    .ForMember(e => e.Guid, x => x.Ignore());
            });
            return automapper;
        }
    }
}