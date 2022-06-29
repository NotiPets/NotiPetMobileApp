namespace NotiPet.Domain.Models
{
    public class Ticket
    {
        public Ticket(string title, string description, string name, string phone, string email)
        {
            Title = title;
            Description = description;
            Name = name;
            Phone = phone;
            Email = email;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}