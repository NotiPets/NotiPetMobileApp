using System;

namespace NotiPet.Data.Dtos
{
    public class UserRoleDto
    {
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
        public string  Password { get; set; }
        public string Email { get; set; }
        public int  UserId { get; set; }
        public string BusinessId { get; set; }
        public int RoleId { get; set; }
        public int RatingId { get; set; }
        public UserDto User { get; set; }
    }
}