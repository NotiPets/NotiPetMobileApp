using System;

namespace NotiPet.Data.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public int Role { get; set; }
        public string BusinessId { get; set; }
        public BusinessDto Business { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DocumentType { get; set; }
        public string Document { get; set; }
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PictureUrl { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}