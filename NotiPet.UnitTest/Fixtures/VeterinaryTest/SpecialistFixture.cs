using System.Collections.Generic;
using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class SpecialistFixture:IBuilder
    {
        public static implicit operator Specialist(SpecialistFixture veterinaryFixture) =>
            veterinaryFixture.Build();

        private Specialist Build() => new Specialist(_id,_user,_speciality);

        public static implicit operator SpecialistDto(SpecialistFixture veterinaryFixture) =>
            veterinaryFixture.BuildDto();

        private SpecialistDto BuildDto() => JsonConvert.DeserializeObject<SpecialistDto>("");
        public SpecialistFixture IdWith(string id) => this.With(ref _id,id);
        public SpecialistFixture UserWith(Domain.Models.User user) => this.With(ref _user,user);
        public SpecialistFixture SpecialistWith(Speciality speciality) => this.With(ref _speciality,speciality);
        private string _id;

        private Domain.Models.User _user;

        private Speciality _speciality;
    }
}