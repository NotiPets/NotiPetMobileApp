using System;
using System.Linq;

namespace NotiPet.Domain.Models
{
    public class AppointmentSale
    {
        public AppointmentSale(Sales sales)
        {
            var order = sales.Orders.FirstOrDefault();
            OrderId = order?.Id;
            Total = sales.Total;
            Veterinary = sales.Veterinary;
         
            Appointment = order?.Appointment;
            AssetService = order?.AssetService;
        }
        public string OrderId { get; set; }
        public Veterinary Veterinary { get; set; }
        public decimal Total { get; set; }
        public DateTime Updated { get; set; }
        public Pet Pet { get; set; }
        public AssetServiceModel AssetService { get; set; }
        public Appointment Appointment { get; set; }
    }
}