using System;

namespace NotiPet.Domain.Models
{
    public class User
    {
        public User(int id, string document, string name, DateTime create, string lastname, DateTime updated, string phone, string address1, string address2, string city, string province, int documentTypeId)
        {
            Id = id;
            Document = document;
            Name = name;
            Create = create;
            Lastname = lastname;
            Updated = updated;
            Phone = phone;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Province = province;
            DocumentTypeId = documentTypeId;
        }

        public int Id { get;  }
        public string Document { get;  }
        public string Name { get;  }
        public DateTime Create { get; }
        public string Lastname { get;  }
        public DateTime Updated { get; }
        public string Phone { get;  }
        public string Address1 { get; }
        public string Address2 { get;  }
        public string City { get; }
        public string  Province { get;}
        public int DocumentTypeId { get; }
    }
}