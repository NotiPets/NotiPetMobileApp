using AutoMapper;
using NotiPet.Data.Services;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class VeterinaryServiceFixture:IBuilder
    {
        public static implicit operator VeterinaryService(VeterinaryServiceFixture veterinaryServiceFixture) =>
            veterinaryServiceFixture.Build();

        private VeterinaryService Build() => new VeterinaryService(_businessService,_mapper);

        public VeterinaryServiceFixture BusinessServiceWith(IBusinessServiceApi businessService) =>
            this.With(ref _businessService, businessService);
        private IBusinessServiceApi _businessService;
        public VeterinaryServiceFixture MapperWith(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private IMapper _mapper;

    }
}