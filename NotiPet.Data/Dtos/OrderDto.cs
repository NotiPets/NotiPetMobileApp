using System;
using Newtonsoft.Json;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Dtos
{
    public class OrderDto
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("assetsServicesId")]
        public int AssetsServicesId { get; set; }

        [JsonProperty("appointment")]
        public AppointmentDto Appointment { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("petId")]
        public string PetId { get; set; }
    }
}