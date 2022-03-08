namespace NotiPet.Data.Dtos
{
    public class RequestSignUpDto
    {
        public UserDto User { get; set; }
        public int Role { get; set; }
        public string BusinessId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}