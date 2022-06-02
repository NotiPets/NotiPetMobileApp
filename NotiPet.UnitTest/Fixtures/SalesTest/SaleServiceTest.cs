using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using NotiPet.Data.Services;

namespace NotiPet.UnitTest.Fixtures.SalesTest
{
    public class SaleServiceFixture:IBuilder
    {
        public static implicit operator SalesService(SaleServiceFixture fixture) => fixture.Build();

        private SalesService Build() => new SalesService(_mapper,_salesService,_connectionBuilder);
        public SaleServiceFixture SalesServiceWith(ISalesServiceApi salesServiceApi) =>
            this.With(ref _salesService, salesServiceApi);
        private ISalesServiceApi _salesService;
        private HubConnection _connectionBuilder ;
        public SaleServiceFixture MapperWith(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private IMapper _mapper;
    }
}

