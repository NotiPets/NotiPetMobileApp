using AutoMapper;
using NotiPet.Data.Services;

namespace NotiPet.UnitTest.Fixtures.SalesTest
{
    public class SaleServiceFixture:IBuilder
    {
        public static implicit operator SalesService(SaleServiceFixture fixture) => fixture.Build();

        private SalesService Build() => new SalesService(_mapper,_salesService);
        public SaleServiceFixture SalesServiceWith(ISalesServiceApi salesServiceApi) =>
            this.With(ref _salesService, salesServiceApi);
        private ISalesServiceApi _salesService;
        public SaleServiceFixture MapperWith(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private IMapper _mapper;
    }
}

