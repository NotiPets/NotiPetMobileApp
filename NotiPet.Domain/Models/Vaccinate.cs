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
    public class DigitalVaccine
    {
    
        public string Id { get; set; }


        public string PetId { get; set; }


        public object Pet { get; set; }


        public int BusinessId { get; set; }


        public Veterinary Business { get; set; }


        public string UserId { get; set; }

   
        public object User { get; set; }

   
        public string VaccineId { get; set; }

   
        public Vaccinate Vaccine { get; set; }


        public DateTime Date { get; set; }
    }
}