using System;

namespace NotiPet.Domain.Models
{
    public class AssetServiceModel
    {
        public AssetServiceModel()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}