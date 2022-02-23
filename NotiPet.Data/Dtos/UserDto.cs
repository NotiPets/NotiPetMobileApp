using System;

namespace NotiPet.Data.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime Create { get; set; }
        public string Lastname { get; set; }
        public DateTime Updated { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string  Province { get; set; }
        public int DocumentTypeId { get; set; }
    }
}