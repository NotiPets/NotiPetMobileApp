namespace NotiPet.Domain.Models
{
    public class RequestSignUp
    {
        public RequestSignUp(User user, int role, string businessId, string username, string password, string email)
        {
            User = user;
            Role = role;
            BusinessId = businessId;
            Username = username;
            Password = password;
            Email = email;
        }

        public User User { get;  }
        public int Role { get;}
        public string BusinessId { get;  }
        public string Username { get;  }
        public string Password { get;  }
        public string Email { get;  }
    }
}