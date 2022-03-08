namespace NotiPet.Data.Dtos
{
    public class AuthenticationDto
    {
        public string Token { get; set; }
        public bool IsRegister { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}