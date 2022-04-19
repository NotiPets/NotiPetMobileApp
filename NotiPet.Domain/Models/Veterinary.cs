namespace NotiPet.Domain.Models
{
    public class Veterinary
    {


        //TODO: FALTA LA FOTO
        public Veterinary( int id, string name,string rnc, string phone, string email, string address1, string address2, string pictureUrl, string city, string province, double latitude, double longitude)
        {
            Name = name;
            Id = id;
            Rnc = rnc;
            Phone = phone;
            Email = email;
            Address1 = address1;
            Address2 = address2;
            PictureUrl = pictureUrl;
            City = city;
            Province = province;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; set; }
        public int Id { get;  }
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

        public string FullAddress => $"{Address1}, {Province},{City}";

        public string Description { get; set; }
    }
}