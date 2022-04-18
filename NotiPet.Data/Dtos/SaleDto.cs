using System;
using System.Collections.Generic;

namespace NotiPet.Data.Dtos
{
    public class SaleDto
    {
        public string Id { get; set; }
        public double Total { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}