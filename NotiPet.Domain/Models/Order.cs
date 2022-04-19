using System;
using System.Collections.Generic;

namespace NotiPet.Domain.Models
{
    public class Order
    {
        public string UserId { get; set; }
        public int AssetsServicesId { get; set; }
        public Appointment Appointment { get; set; }
        public AssetServiceModel AssetService { get; set; }
        public int Quantity { get; set; }
        public string PetId { get; set; }
        public Order(string userId, int assetsServicesId, Appointment appointment, int quantity,string petId,AssetServiceModel assetService)
        {
            PetId = petId;
            UserId = userId;
            AssetsServicesId = assetsServicesId;
            AssetService = assetService;
            Appointment = appointment;
            Quantity = quantity;
        }


    }
    public class RequestOrder
    {
        public RequestOrder(List<Order> orders, int businessId)
        {
            Orders = orders;
            BusinessId = businessId;
        }

        public List<Order> Orders { get;  }
        public int BusinessId { get; set; }
    }
    public class Sales
    {
        public Sales(string id, int total, DateTime created, DateTime updated, List<Order> orders, int businessId, Veterinary business)
        {
            Id = id;
            Total = total;
            Created = created;
            Updated = updated;
            Orders = orders;
            BusinessId = businessId;
            Veterinary = business;
        }

        public string Id { get;  }
        public int Total { get;  }
        public DateTime Created { get;  }
        public DateTime Updated { get;  }
        public int BusinessId { get; set; }
        public Veterinary Veterinary { get; set; }
   
        public List<Order> Orders { get;  }
    }
}