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


        private Pet Build()=>new Pet(_create,_update,_active,_name,_pictureUrl,_birthDate,_description,_userId,_petTypeId,_petType);

        public PetFixture WithCreate(DateTime create) => this.With(ref _create, create);
        public PetFixture WithUpdate(DateTime update) => this.With(ref _update, update);
        public PetFixture WithBirthDate(DateTime birthDate) => this.With(ref _birthDate, birthDate);
        public PetFixture WithActive(bool active) => this.With(ref _active, active);
        public PetFixture WithName(string name) => this.With(ref _name,name);
        public PetFixture WithPictureUrl(string pictureUrl) => this.With(ref _pictureUrl, pictureUrl);
        public PetFixture WithDescription(string description) => this.With(ref _description,description);
        public PetFixture WithUserId(int userId) => this.With(ref _userId,userId);
        public PetFixture WithPetTypeId(int petTypeId) => this.With(ref _petTypeId,petTypeId);
        public PetFixture WithPetType(PetType petType) => this.With(ref _petType,petType);
        

        private DateTime _create;
        private DateTime _update ;
        private bool _active;
        private string _name ;
        private string _pictureUrl;
        private DateTime _birthDate;
        private string _description;
        private int _userId;
        private int _petTypeId ;
        private PetType _petType;
    }
}