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
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    public class PetSize
    {
        public PetSize(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get;  }
        public string Name { get;  }
        public override string ToString()
        {
            return $"{Name}";
        }
    } 
}