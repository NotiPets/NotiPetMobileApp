using System;

namespace NotiPet.Domain.Models
{
    
    public class User
    {
       public void ConvertToEditUserRequest(IEditUserRequest editUserRequest)
       {
           editUserRequest.Names = Names;
           editUserRequest.Lastnames = Lastnames;
           editUserRequest.Phone = Phone;
           editUserRequest.PictureUrl = PictureUrl;
           editUserRequest.BusinessId =BusinessId;
           editUserRequest.Username = Username;
           editUserRequest.Email = Email;
        
       }
       public void SetToEditUserRequest(IEditUserRequest editUserRequest)
       {
           Names = editUserRequest.Names;
           Lastnames = editUserRequest.Lastnames;
           Phone =editUserRequest. Phone;
           PictureUrl =editUserRequest. PictureUrl;
           BusinessId =editUserRequest.BusinessId;
           Username = editUserRequest.Username;
           
           Email = Email;
           
       }
        public User(string id, int role, int businessId, Veterinary business, string username, string password, string email, int documentType, string document, string names, string lastnames, string phone, string address1, string address2, string city, string province, string pictureUrl, bool active, DateTime created, DateTime updated)
        {
            Id = id;
            Role = role;
            BusinessId = businessId;
            Business = business;
            Username = username;
            Password = password;
            Email = email;
            DocumentType = documentType;
            Document = document;
            Names = names;
            Lastnames = lastnames;
            Phone = phone;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Province = province;
            PictureUrl = pictureUrl;// "https://www.hogarmania.com/archivos/202008/como-llevar-perro-veterinario-portada-668x400x80xX-1.jpg";
            Active = active;
            Created = created;
            Updated = updated;
        }

        public string Id { get; set; }
        public int Role { get; set; }
        public int BusinessId { get; set; }
        public Veterinary Business { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DocumentType { get; set; }
        public string Document { get; set; }
        public string Names { get; set; }
        public string Lastnames { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PictureUrl { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string FullName => $"{Names} {Lastnames}";
        public string FullAddress => $"{City}, {Province}, {Address1}, {Address2}";

        public string Description { get; set; }


    }
}