using System;

namespace NotiPet.Data.Dtos
{
    public class AssetServiceDto
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}