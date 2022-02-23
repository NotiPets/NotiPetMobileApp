using System;

namespace NotiPet.Data.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public bool IsEmergency { get; set; }
        public int Pet { get; set; }
        public int AppointmentStatusId { get; set; }
        public int AssetServiceId { get; set; }
        
    }
}