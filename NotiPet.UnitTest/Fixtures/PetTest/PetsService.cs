using AutoMapper;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;

namespace NotiPet.UnitTest.Fixtures
{
    public class PetsServiceFixture:IBuilder
    {
        public static implicit operator PetsService(PetsServiceFixture petsServiceFixture) => petsServiceFixture.Build();
        private PetsService Build() => new PetsService(_petService,_mapper);
        

        public PetsServiceFixture PetServiceApiWith(IPetServiceApi petServiceApi) =>
            this.With(ref _petService, petServiceApi);
        private IPetServiceApi _petService;
        public PetsServiceFixture MapperWith(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private IMapper _mapper;


    }
}