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
    public class DigitalVaccineDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("petId")]
        public string PetId { get; set; }

        [JsonProperty("pet")]
        public object Pet { get; set; }

        [JsonProperty("businessId")]
        public int BusinessId { get; set; }

        [JsonProperty("business")]
        public BusinessDto Business { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }

        [JsonProperty("vaccineId")]
        public string VaccineId { get; set; }

        [JsonProperty("vaccine")]
        public VaccinateDto Vaccine { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}