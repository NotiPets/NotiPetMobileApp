using System;
using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.UnitTest.Fixtures
{
    public class PetFixture:IBuilder
    {
        public static implicit operator Pet(PetFixture fixture) => fixture.Build();
        public static implicit operator PetDto(PetFixture fixture) => fixture.BuildDto();

        private PetDto BuildDto() => JsonConvert.DeserializeObject<PetDto>("");


        private Pet Build()=>
            new Pet(_id,_name,_petType,_petTypeName,
                _userId,_user,_size,_active,_pictureUrl,_description,_gender,_vaccinated,_castrated,_hasTracker,_birthdate,_created,_updated);

        public PetFixture WithCreate(DateTime create) => this.With(ref _created, create);
        public PetFixture WithUpdate(DateTime update) => this.With(ref _updated, update);
        public PetFixture WithBirthDate(DateTime birthDate) => this.With(ref _birthdate, birthDate);
        public PetFixture WithActive(bool active) => this.With(ref _active, active);
        public PetFixture WithName(string name) => this.With(ref _name,name);
        public PetFixture WithPictureUrl(string pictureUrl) => this.With(ref _pictureUrl, pictureUrl);
        public PetFixture WithDescription(string description) => this.With(ref _description,description);
        public PetFixture WithUserId(string  userId) => this.With(ref _userId,userId);
        public PetFixture WithPetTypeId(int petTypeId) => this.With(ref _petType,petTypeId);
        public PetFixture WithPetType(string petType) => this.With(ref _petTypeName,petType);
        

        private string _id;
        private string _name;
        private int _petType;
        private string _petTypeName;
        private string _userId;
        private Domain.Models.User _user;
        private int _size;
        private bool _active;
        private string _pictureUrl;
        private string _description;
        private bool _gender;
        private bool _vaccinated;
        private bool _castrated;
        private bool _hasTracker;
        private DateTime _birthdate;
        private DateTime _created;
        private DateTime _updated;


    }
}