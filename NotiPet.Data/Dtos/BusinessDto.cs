using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class BusinessDto
    {
        [JsonProperty("businessName")]
        public string Name { get; set; }
        public int Id { get; set; }
        public string Rnc { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PictureUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
 
        [JsonProperty("comment")]
        public string Description { get; set; }

        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        
    }
}