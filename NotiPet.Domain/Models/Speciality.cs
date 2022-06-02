namespace NotiPet.Domain.Models
{
    public class Speciality
    {

        public int Id { get;  }

        public string Name { get;  }

        public string Description { get;  }
        
        //TODO: FALTA EXPERIENCIA DEL ESPECIALISTA 

        public Speciality(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}