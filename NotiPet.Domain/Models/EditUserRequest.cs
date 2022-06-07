namespace NotiPet.Domain.Models
{
    public interface IEditUserRequest
    {
        public string Names { get; set; }
        public string Username { get; set; }
        public string Lastnames { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BusinessId { get; set; }
        public string PictureUrl { get; set; }
    }
}