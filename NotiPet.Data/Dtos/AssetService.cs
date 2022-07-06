using System;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Dtos
{
    public class AssetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Vendor { get; set; }
        public bool Active { get; set; }
        public int AssetsServiceType { get; set; }
        public int BusinessId { get; set; }
        public BusinessDto Business { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string PictureUrl { get; set; }
    }
}