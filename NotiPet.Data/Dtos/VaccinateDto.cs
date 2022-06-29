using System;
using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class VaccinateDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("petType")]
        public object PetType { get; set; }

        [JsonProperty("vaccineName")]
        public string VaccineName { get; set; }

        [JsonProperty("businessId")]
        public int BusinessId { get; set; }

        [JsonProperty("business")]
        public BusinessDto Business { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}