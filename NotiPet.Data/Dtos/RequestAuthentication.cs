using System.Text.Json.Serialization;

namespace NotiPet.Data.Dtos
{
    public class RequestAuthenticationDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}