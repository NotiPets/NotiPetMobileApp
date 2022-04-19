using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class SpecialistDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public UserDto User { get; set; }

        [JsonProperty("speciality")]
        public SpecialityDto Speciality { get; set; }
    }
}