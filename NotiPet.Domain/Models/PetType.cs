namespace NotiPet.Domain.Models
{
    public class PetType
    {
        public PetType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get;  }
        public string Name { get;  }
    }
}