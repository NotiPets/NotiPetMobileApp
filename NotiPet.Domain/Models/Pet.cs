using System;

namespace NotiPet.Domain.Models
{
    public class Pet
    {
        public Pet(DateTime create, DateTime update, bool active, string name, string pictureUrl, DateTime birthDate, string description, int userId, int petTypeId, PetType petType)
        {
            Create = create;
            Update = update;
            Active = active;
            Name = name;
            PictureUrl = pictureUrl;
            BirthDate = birthDate;
            Description = description;
            UserId = userId;
            PetTypeId = petTypeId;
            PetType = petType;
        }

        public DateTime Create { get;  }
        public DateTime Update { get; }
        public bool Active { get;  }
        public string Name { get; }
        public string PictureUrl { get;  }
        public DateTime BirthDate { get;  }
        public string Description { get;  }
        public int UserId { get;  }
        public int PetTypeId { get; }
        public PetType PetType { get;  }
    }
}