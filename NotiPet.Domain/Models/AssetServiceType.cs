namespace NotiPet.Domain.Models
{
    public class AssetServiceType
    {
        public AssetServiceType(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; }
        public string Description { get;  }
    }
}