using AutoMapper;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class SpecialistServiceFixture:IBuilder
    {
        public static implicit operator SpecialistsService(SpecialistServiceFixture veterinaryFixture) =>
            veterinaryFixture.Build();

        private SpecialistsService Build() => new SpecialistsService(_mapper, new SpecialistsServiceApiMock());
        public SpecialistServiceFixture WithMapper(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private  IMapper _mapper;
    }
}