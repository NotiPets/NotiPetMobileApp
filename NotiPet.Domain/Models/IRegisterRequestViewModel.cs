namespace NotiPet.Domain.Models
{
    public interface IRegisterRequestViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int  DocumentType { get; set; }
        public string Document { get; set; }
        public string BusinessId { get; set; }
    }
}