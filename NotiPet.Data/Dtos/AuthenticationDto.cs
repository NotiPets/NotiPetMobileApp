using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class AuthenticationDto
    {
        [JsonProperty("jwt")]
        public string Jwt { get; set; }

        [JsonProperty("user")]
        public UserDto User { get; set; }
    }
}