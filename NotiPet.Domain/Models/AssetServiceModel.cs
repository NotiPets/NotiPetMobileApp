using System;

namespace NotiPet.Domain.Models
{
    public class AssetServiceModel
    {
        public AssetServiceModel(int id, string name, string description, decimal price, int quantity, int vendor, bool active, int assetsServiceType, int businessId, object business, DateTime? created, DateTime? updated)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            Vendor = vendor;
            Active = active;
            AssetsServiceType = assetsServiceType;
            BusinessId = businessId;
            Business = business;
            Created = created;
            Updated = updated;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Vendor { get; set; }
        public bool Active { get; set; }
        public int AssetsServiceType { get; set; }
        public int BusinessId { get; set; }
        public object Business { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public AssetsServiceTypeId AssetServiceType => (AssetsServiceTypeId) AssetsServiceType;
        public string PictureUrl { get; set; } = "notImage";
        public override string ToString()
        {
            return $"{Name} - {Price:C2}";
        }
    }
    public enum AssetsServiceTypeId
    {
        Product,
        Service
    }
}