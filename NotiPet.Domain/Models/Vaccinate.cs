using System;

namespace NotiPet.Domain.Models
{
    public class Vaccinate
    {
 
        public string Id { get; set; }

     
        public object PetType { get; set; }

        public string VaccineName { get; set; }


        public int BusinessId { get; set; }


        public Veterinary Business { get; set; }

        
        public DateTime Date { get; set; }
    }
}