namespace NotiPet.Domain.Models
{
    public class Specialist
    {

        public string Id { get;  }

        public User User { get;  }

        public Speciality Speciality { get; }

        public Specialist(string id, User user, Speciality speciality)
        {
            Id = id;
            User = user;
            Speciality = speciality;
        }
    }
}