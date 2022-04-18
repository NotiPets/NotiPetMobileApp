using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.UnitTest.Fixtures.SalesTest
{
    public class OrderFixture:IBuilder
    {
        public static implicit operator Order(OrderFixture fixture)=>fixture.Build();
        public static implicit operator OrderDto(OrderFixture fixture)=>fixture.BuildDto();

        private OrderDto BuildDto() => JsonConvert.DeserializeObject<OrderDto>("");

        public Order Build() => new Order(_userId,_assetsServicesId,_appointment,_quantity,string.Empty);
        public OrderFixture UserIdWith(string userId) => this.With(ref _userId, userId);
        public OrderFixture AssetsServicesWith(int assertService) => this.With(ref _assetsServicesId, assertService);
        public OrderFixture AppointmentWith(Appointment appointment) => this.With(ref _appointment, appointment);
        public OrderFixture QuantityWith(int quantity) => this.With(ref _quantity, quantity);
        private string _userId;
        private int _assetsServicesId;
        private Appointment _appointment;
        private int _quantity;
    }
}