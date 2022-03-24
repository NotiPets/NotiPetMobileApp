namespace NotiPet.Domain.Models
{
    public class Veterinary
    {
        public Veterinary(string id, string name,string rnc, string phone, string email, string address1, string address2, string city, string province, double latitude, double longitude)
        {
            Name = name;
            Id = id;
            Rnc = rnc;
            Phone = phone;
            Email = email;
            Address1 = /*address1*/" Av. Winston Churchill 1452, Santo Domingo 10130";
            Address2 = address2;
            City = city;
            Province = province;
            Latitude = latitude;
            Longitude = longitude;
            PictureUrl = /*pictureUrl*/ "https://pbs.twimg.com/profile_images/480340277052182528/WdPmb366_400x400.jpeg";
        }

        //TODO: FALTA LA FOTO
        public string Name { get; set; }
        public string Id { get;  }
        public string Rnc { get;  }
        public string Phone { get; }
        public string Email { get;  }
        public string Address1 { get; }
        public string Address2 { get;  }
        public string PictureUrl { get;  }

        public string City { get;  }
        public string Province { get;  }
        public double Latitude { get;  }
        public double Longitude { get; }
    }
}