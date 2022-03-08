using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
namespace NotiPet.UnitTest.Fixtures.VeterinaryTest

{
    public class VeterinaryFixture : IBuilder
    {
        public static implicit operator Veterinary(VeterinaryFixture veterinaryFixture) =>
            veterinaryFixture.Build();
        public static implicit operator BusinessDto(VeterinaryFixture veterinaryFixture) =>
            veterinaryFixture.BuildDto();

        private BusinessDto BuildDto() => JsonConvert.DeserializeObject<BusinessDto>("");

        private Veterinary Build() => new Veterinary(_id,_name,_rnc,_phone,_email,_address1,_address2,_city,_province,_latitude,_longitude);
        public VeterinaryFixture IdWith(string id) => this.With(ref _id, id);
        public VeterinaryFixture RncWith(string rnc) => this.With(ref _rnc, rnc);
        public VeterinaryFixture NameWith(string name) => this.With(ref _name, name);
        public VeterinaryFixture PhoneWith(string phone) => this.With(ref _phone, phone);
        public VeterinaryFixture EmailWith(string email) => this.With(ref _email,email);
        public VeterinaryFixture Address1With(string address1) => this.With(ref _address1, address1);
        public VeterinaryFixture Address2With(string address2) => this.With(ref _address2,address2);
        public VeterinaryFixture CityWith(string city) => this.With(ref _city, city);
        public VeterinaryFixture ProvinceWith(string province) => this.With(ref _province, province);
        public VeterinaryFixture LatitudeWith(double latitude) => this.With(ref _latitude, latitude);
        public VeterinaryFixture LongitudeWith(double longitude) => this.With(ref _longitude, longitude);
        
        private string _id;
        private string _rnc;
        private string _name;
        private string _phone ;
        private string _email ;
        private string _address1 ;
        private string _address2 ;
        private string _city ;
        private string _province ;
        private double _latitude;
        private double _longitude;

    }
}