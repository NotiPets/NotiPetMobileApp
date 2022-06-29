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
        public bool CantEdit =>   DateTime.Now.Subtract(Date).TotalHours < 24;

    }
    public class CreateAppointment
    {
        public CreateAppointment(DateTime date, string petId, string userId)
        {
            Date = date;
            PetId = petId;
            UserId = userId;
        }

        public DateTime Date { get; set; }
        public bool IsEmergency { get; set; }
        public string PetId { get; set; }
        public int AppointmentStatusId { get; set; }
        public int? AssetServiceId { get; set; }
        public string UserId { get; set; }
        public int BusinessId { get; set; }
    }

   public enum EAppointmentStatus
    {
        Requested = 0,
        Accepted = 1,
        Cancelled = 2,
        Completed = 3,
        Denied = 4
        
    }
    
    
}