using System;

namespace NotiPet.Data.Dtos
{
    public class AppointmentDto
    {
        public string Id { get; set; }
        public string SpecialistId { get; set; }
        public SpecialistDto Specialist { get; set; }
        public int AppointmentStatus { get; set; }
        public bool IsEmergency { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        
    }
    
}