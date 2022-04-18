using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotiPet.Data.Dtos
{
    public class RequestOrderDto
    {

        [JsonProperty("orders")]
        public List<OrderDto> Orders { get; set; }

        [JsonProperty("businessId")]
        public int BusinessId { get; set; }
    }
}