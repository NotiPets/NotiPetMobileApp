using System;

namespace NotiPet.Domain.Models
{
    public class UserRole
    {
        public UserRole(DateTime create, DateTime update, bool active, string username, string password, string email, int userId, string businessId, int roleId, int ratingId,User user)
        {
            Create = create;
            Update = update;
            Active = active;
            Username = username;
            Password = password;
            Email = email;
            UserId = userId;
            BusinessId = businessId;
            RoleId = roleId;
            RatingId = ratingId;
            User = user;
        }


        public DateTime Create { get;  }
        public DateTime Update { get;  }
        public bool Active { get;  }
        public string Username { get;  }
        public string  Password { get; }
        public string Email { get;  }
        public int  UserId { get;  }
        public string BusinessId { get; }
        public int RoleId { get;  }
        public int RatingId { get;  }
        public User User { get; }
    }
}