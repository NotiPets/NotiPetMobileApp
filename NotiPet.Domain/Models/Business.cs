namespace NotiPet.Domain.Models
{
    public class Business
    {
        public Business(int id, string rnc, string phone, string email, string address1, string address2, string city, string province)
        {
            Id = id;
            Rnc = rnc;
            Phone = phone;
            Email = email;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Province = province;
        }

        public int Id { get;  }
        public string Rnc { get;  }
        public string Phone { get; }
        public string Email { get;  }
        public string Address1 { get; }
        public string Address2 { get;  }
        public string City { get;  }
        public string Province { get;  }
    }
}