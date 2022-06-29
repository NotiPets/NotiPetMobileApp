using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class PetsService:IPetsService
    {
        private readonly IPetServiceApi _petService;
        private readonly IMapper _mapper;
        private SourceCache<Pet, string> _petSource = new SourceCache<Pet, string>(e => e.Id);
        private SourceList<Vaccinate> _vaccinateSource = new SourceList<Vaccinate>();
        
        public PetsService(IPetServiceApi petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
            PetInformations= new List<PetInformation>
            {
                new PetInformation {Name = "Vaccinated", Status = false},
                new PetInformation {Name = "Castrated", Status = false},
                new PetInformation{Name = "HasTracker", Status = false }
            };
            PetTypes = new List<PetType>()
            {
                new PetType((int) EPetTypeId.Bunny, $"{EPetTypeId.Bunny}"),
                new PetType((int) EPetTypeId.Cat, $"{EPetTypeId.Cat}"),
                new PetType((int) EPetTypeId.Dog, $"{EPetTypeId.Dog}"),
                new PetType((int) EPetTypeId.Other, $"{EPetTypeId.Other}"),
            };
            PetSizes = new List<PetSize>()
            {
                new PetSize(0, "Small"),
                new PetSize(1, "Medium"),
                new PetSize(2, "Large"),

            };
        }


        public IObservable<IEnumerable<Pet>> GetPets(string userId)
        {
            return _petService.GetPets(userId)
                .Select(_mapper.Map<IEnumerable<Pet>>)
                .Do(_petSource.AddOrUpdate);
        }

        public IObservable<Pet> SavePet(CreatePetModel createPetModel)
        {
            createPetModel.Castrated = (PetInformations.FirstOrDefault(x => x.Name == "Castrated")?.Status).GetValueOrDefault();
            createPetModel.Vaccinated = (PetInformations.FirstOrDefault(x => x.Name == "Vaccinated")?.Status).GetValueOrDefault();
            createPetModel.HasTracker = (PetInformations.FirstOrDefault(x => x.Name == "HasTracker")?.Status).GetValueOrDefault();
            var pet = new Pet(Guid.NewGuid().ToString(), createPetModel.Name
                , createPetModel.PetType.GetValueOrDefault(), null, createPetModel.User,
                null, createPetModel.Size.GetValueOrDefault(), true, createPetModel.PictureUrl, createPetModel.Description,
                createPetModel.Gender, createPetModel.Vaccinated,  createPetModel.Castrated,
                createPetModel.HasTracker, createPetModel.Birthdate, DateTime.Now, DateTime.Now);
            return _petService.SavePets(_mapper.Map<PetDto>(pet))
                .Select(_mapper.Map<Pet>);
        }
        
        public SourceCache<Pet, string> Pets => _petSource;
        public SourceList<Vaccinate> Vaccinate => _vaccinateSource;
        public IObservable<VaccinatePdf> GetVaccinePdf(string vaccinateId)
        {
            return _petService.GetVaccinePdf(vaccinateId)
                .Select(_mapper.Map<VaccinatePdf>);
        }

        public Func<Pet, bool> SearchPredicate(string text) =>
            pet => string.IsNullOrEmpty(text)|| (pet.Name.Contains(text));

        public List<PetInformation> PetInformations { get; }
        public List<PetType> PetTypes { get; }
        public List<PetSize> PetSizes { get; }
        public IObservable<object> RemovePet(string id)
        {
            _petSource.Remove(id);
            return _petService.RemovePets(id);
        }

        public IObservable<Pet> EditPet(CreatePetModel createPetModel)
        {
            createPetModel.Castrated = (PetInformations.FirstOrDefault(x => x.Name == "Castrated")?.Status).GetValueOrDefault();
            createPetModel.Vaccinated = (PetInformations.FirstOrDefault(x => x.Name == "Vaccinated")?.Status).GetValueOrDefault();
            createPetModel.HasTracker = (PetInformations.FirstOrDefault(x => x.Name == "HasTracker")?.Status).GetValueOrDefault();
            var pet = new Pet(createPetModel.Id, createPetModel.Name
                , createPetModel.PetType.GetValueOrDefault(), null, createPetModel.User,
                null, createPetModel.Size.GetValueOrDefault(), true, createPetModel.PictureUrl, createPetModel.Description,
                createPetModel.Gender, createPetModel.Vaccinated,  createPetModel.Castrated,
                createPetModel.HasTracker, createPetModel.Birthdate, DateTime.Now, DateTime.Now);
            return _petService.EditPet(_mapper.Map<PetDto>(pet))
                .Select(_mapper.Map<Pet>);
        }

        public IObservable<IEnumerable<Vaccinate>> GetVaccinesByPet(string petId)
        {
            Vaccinate.Clear();
            return _petService.GetVaccinesByPet(petId)
                .Select(_mapper.Map<IEnumerable<Vaccinate>>)
                .Do(_vaccinateSource.AddRange);
        }
    }
}