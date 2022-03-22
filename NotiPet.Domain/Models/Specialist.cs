namespace NotiPet.Domain.Models
{
    public class Specialist
    {

        public int Id { get;  }

        public User User { get;  }

        public Speciality Speciality { get; }

        public Specialist(int id, User user, Speciality speciality)
        {
            Id = id;
            User = user;
            Speciality = speciality;
        }
    }
}