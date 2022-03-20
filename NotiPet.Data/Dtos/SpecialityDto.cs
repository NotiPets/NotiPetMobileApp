using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class SpecialityDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}