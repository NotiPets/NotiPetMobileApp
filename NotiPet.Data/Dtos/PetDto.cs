using System;

namespace NotiPet.Data.Dtos
{
    public class PetDto
    {
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int PetTypeId { get; set; }
        public PetTypeDto PetType{ get; set; }
    }
}