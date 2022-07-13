using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class VaccinatePdfDto
    {
        [JsonProperty("response")]
        public string File { get; set; }
    }

}