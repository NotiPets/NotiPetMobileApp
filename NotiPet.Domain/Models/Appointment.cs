using System;

namespace NotiPet.Domain.Models
{
    public class Appointment
    {
        public Appointment(string id, string specialistId, Specialist specialist, EAppointmentStatus appointmentStatus, bool isEmergency, bool active, DateTime date, DateTime updated)
        {
            Id = id;
            SpecialistId = specialistId;
            Specialist = specialist;
            AppointmentStatus = appointmentStatus;
            IsEmergency = isEmergency;
            Active = active;
            Date = date;
            Created = DateTime.Now;
            Updated = updated;
        }

        public string Id { get;  }
        public string SpecialistId { get; }
        public Specialist Specialist { get;  }
        public EAppointmentStatus AppointmentStatus { get;  }
        public bool IsEmergency { get;  }
        public bool Active { get;  }
        public DateTime Date { get;  }
        public DateTime Created { get;}
        public DateTime Updated { get; }

    }
    public class CreateAppointment
    {

        public DateTime Date { get; set; }
        public bool IsEmergency { get; set; }
        public int PetId { get; set; }
        public int AppointmentStatusId { get; set; }
        public int AssetServiceId { get; set; }

    }

   public enum EAppointmentStatus
    {
        Requested = 0,
        Accepted = 1,
        Cancelled = 2
        
    }
    
    
}