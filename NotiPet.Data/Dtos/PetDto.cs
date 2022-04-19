using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class PetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("petType")]
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
        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddThh:mm:ss.000Z")]
        public DateTime Birthdate { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddThh:mm:ss.000Z")]
        public DateTime Created { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddThh:mm:ss.000Z")]
        public DateTime Updated { get; set; }
    }
}