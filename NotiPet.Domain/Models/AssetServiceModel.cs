using System;

namespace NotiPet.Domain.Models
{
    public class AssetServiceModel
    {

        public AssetServiceModel( int id, string name, string description, decimal price, string pictureUrl, int quantity, bool active, DateTime? created, DateTime? updated ,int vendedorId, int assetServiceTypeId, int businessId, AssetServiceType assetServiceType)
        {
            Guid = Guid.NewGuid();
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;
            Quantity = quantity;
            Active = active;
            Created = created;
            Updated = updated;
            VendedorId = vendedorId;
            AssetServiceTypeId = assetServiceTypeId;
            BusinessId = businessId;
            AssetServiceType = assetServiceType;
        }

        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }            
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public bool Active { get;  set;}
        
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int VendedorId { get;set; }
        public int AssetServiceTypeId { get; set; }
        public AssetServiceType AssetServiceType { get; set; }
        public int BusinessId { get; set; }
    }
}