using System;
using System.Text.Json.Serialization;

namespace NotiPet.Data.Dtos
{
    public class PetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("petType")]
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public int Size { get; set; }
        public bool Active { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public bool Gender { get; set; }
        public bool Vaccinated { get; set; }
        public bool Castrated { get; set; }
        public bool HasTracker { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}