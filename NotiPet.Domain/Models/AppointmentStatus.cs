namespace NotiPet.Domain.Models
{
    public class AppointmentStatus
    {
        public AppointmentStatus(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; }
        public string Description { get;  }
    }
} 