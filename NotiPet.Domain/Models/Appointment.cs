using System;

namespace NotiPet.Domain.Models
{
    public class Appointment
    {
        public Appointment(int id, DateTime created, DateTime update, bool active, DateTime date, bool isEmergency, int pet, int appointmentStatusId, int assetServiceId)
        {
            Id = id;
            Created = created;
            Update = update;
            Active = active;
            Date = date;
            IsEmergency = isEmergency;
            Pet = pet;
            AppointmentStatusId = appointmentStatusId;
            AssetServiceId = assetServiceId;
        }

        public int Id { get;  }
        public DateTime Created { get; }
        public DateTime Update { get;  }
        public bool Active { get;  }
        public DateTime Date { get; }
        public bool IsEmergency { get; }
        public int Pet { get;  }
        public int AppointmentStatusId { get; }
        public int AssetServiceId { get;  }

    }
}